# Service Page - Email Subscription Form Implementation Guide

## Overview

This guide will walk you through creating an email subscription form section for your Service Page in Umbraco CMS. The section includes:

- **Centered Form Box**: Dark gray form card with "How can we help you?" heading, description text, and email input with envelope icon

---

## Architecture Decision

Based on your project structure, we'll use the **BlockList Component Pattern** which is consistent with:

- Your existing Service Page structure (uses `@await Html.GetBlockListHtmlAsync(Model.Sections)`)
- Other components like `ExperienceWithFormSection`, `ContactFormSection`
- Content editor flexibility

---

## Step-by-Step Implementation

### Step 1: Create Content Type in Umbraco Backoffice

1. **Login to Umbraco**

   - Navigate to the Umbraco backoffice (typically `/umbraco`)

2. **Create New Element Type**

   - Go to **Settings** → **Document Types**
   - Click **Create** → **Document Type with Template** = NO (it's an Element Type)
   - Click **Element Type**

3. **Configure Element Type**

   - **Name**: `Email Subscription Form Section`
   - **Alias**: `emailSubscriptionFormSection` (auto-generated)
   - **Icon**: Choose `icon-mail` or `icon-plugin`
   - **Folder**: Select "Items" (to keep it organized with other BlockList items)
   - Check **"Is an Element Type"** = YES

4. **Add Properties Tab**

   - Click **Add Group**
   - **Group Name**: `Content`

5. **Add Properties** (Click "Add property" for each):

   **Property 1: Form Heading**

   - Name: `Form Heading`
   - Alias: `formHeading`
   - Data Type: `Textstring`
   - Description: "Form heading (e.g., 'How can we help you?')"
   - Mandatory: No

   **Property 2: Form Description**

   - Name: `Form Description`
   - Alias: `formDescription`
   - Data Type: `Textarea`
   - Description: "Form description (e.g., 'online support 24/7, we are here to help you')"
   - Mandatory: No

6. **Save** the Document Type

---

### Step 2: Add to BlockList Configuration

1. **Find BlockList Data Type**

   - Go to **Settings** → **Data Types**
   - Find `Onatrix Sections` (or the BlockList used by Service Page - check GUID `6a462304-a854-4115-954b-402e6d3f6f34`)

2. **Add New Block Type**

   - Click on the Data Type to edit
   - Under **Available Blocks**, click **Add**
   - Select `Email Subscription Form Section`
   - Configure:
     - **Label**: `Email Subscription Form Section`
     - **Background Color**: Choose a color (optional)
   - Click **Submit**

3. **Save** the Data Type

---

### Step 3: Create CSHTML Partial View

Create the file at the following path:

```
Onatrix_CMS/Views/Partials/blocklist/Components/EmailSubscriptionFormSection.cshtml
```

**File Content:**

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<Umbraco.Cms.Core.Models.Blocks.BlockListItem>

@{
    var content = (EmailSubscriptionFormSection)Model.Content;
    var formHeading = content.FormHeading ?? "How can we help you?";
    var formDescription = content.FormDescription ?? "online support 24/7, we are here to help you";
}

<section class="email-subscription-section">
    <div class="email-subscription-container">
        <div class="email-subscription-card">
            @if (!string.IsNullOrEmpty(formHeading))
            {
                <h3 class="email-subscription-heading">@formHeading</h3>
            }

            @if (!string.IsNullOrEmpty(formDescription))
            {
                <p class="email-subscription-description">@formDescription</p>
            }

            <form class="email-subscription-form" method="post" action="/umbraco/surface/emailsubscription/subscribe">
                @Html.AntiForgeryToken()

                <div class="email-subscription-input-wrapper">
                    <input type="email"
                           name="email"
                           placeholder="E-mail address"
                           required
                           class="email-subscription-input" />
                    <button type="submit" class="email-subscription-submit-btn" aria-label="Submit">
                        <svg class="email-subscription-icon" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
                            <path d="M2.003 5.884L10 9.882l7.997-3.998A2 2 0 0016 4H4a2 2 0 00-1.997 1.884z"/>
                            <path d="M18 8.118l-8 4-8-4V14a2 2 0 002 2h12a2 2 0 002-2V8.118z"/>
                        </svg>
                    </button>
                </div>
            </form>
        </div>
    </div>
</section>
```

---

### Step 4: Create CSS Stylesheet

Create or add to the file:

```
Onatrix_CMS/wwwroot/css/services.css
```

If the file doesn't exist, create it. If it exists, add this content:

**File Content:**

```css
/* ===================================
   Email Subscription Form Section
   =================================== */

.email-subscription-section {
  padding: 4rem 0;
  background-color: #ffffff;
}

.email-subscription-container {
  max-width: 1280px;
  margin: 0 auto;
  padding: 0 1rem;
  display: flex;
  justify-content: center;
  align-items: center;
}

/* Form Card */
.email-subscription-card {
  background-color: #535656;
  padding: 2.5rem;
  border-radius: 4px;
  width: 100%;
  max-width: 400px;
}

.email-subscription-heading {
  font-size: 1.5rem;
  font-weight: 700;
  color: #f7f7f7;
  margin: 0 0 0.75rem 0;
}

.email-subscription-description {
  font-size: 14px;
  line-height: 1.6;
  color: #d4d4d4;
  margin: 0 0 1.5rem 0;
}

/* Email Form Input */
.email-subscription-form {
  width: 100%;
}

.email-subscription-input-wrapper {
  position: relative;
  width: 100%;
}

.email-subscription-input {
  width: 100%;
  padding: 0.875rem 3rem 0.875rem 1rem;
  font-size: 14px;
  color: #535656;
  border: none;
  border-radius: 3px;
  background-color: #ffffff;
  outline: none;
  font-style: italic;
}

.email-subscription-input::placeholder {
  color: #9c9ea6;
  font-style: italic;
}

.email-subscription-input:focus {
  outline: 2px solid #f2eddc;
  outline-offset: 2px;
}

/* Submit Button (Envelope Icon) */
.email-subscription-submit-btn {
  position: absolute;
  right: 0.5rem;
  top: 50%;
  transform: translateY(-50%);
  background: none;
  border: none;
  padding: 0.5rem;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: opacity 0.3s ease;
}

.email-subscription-submit-btn:hover {
  opacity: 0.7;
}

.email-subscription-icon {
  width: 20px;
  height: 20px;
  color: #535656;
}

/* Responsive Design */
@media (max-width: 767px) {
  .email-subscription-section {
    padding: 3rem 0;
  }

  .email-subscription-heading {
    font-size: 1.25rem;
  }

  .email-subscription-card {
    padding: 2rem;
    max-width: 100%;
  }
}
```

---

### Step 5: Link CSS to Layout

Edit your main layout file to include the new CSS:

```
Onatrix_CMS/Views/mainLayout.cshtml
```

Add this line in the `<head>` section (or where other CSS files are linked):

```html
<link rel="stylesheet" href="~/css/services.css" />
```

Or if you prefer to add it only to the Service Page, edit:

```
Onatrix_CMS/Views/ServicePage.cshtml
```

Add this at the top after the `@{` block:

```cshtml
@section Styles {
    <link rel="stylesheet" href="~/css/services.css" />
}
```

(Note: Your layout must support `@RenderSection("Styles", required: false)` in the `<head>`)

---

### Step 6: Create Form Handler (Surface Controller)

Create a new file:

```
Onatrix_CMS/Controllers/Surface/EmailSubscriptionController.cs
```

**File Content:**

```csharp
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix_CMS.Controllers.Surface
{
    public class EmailSubscriptionController : SurfaceController
    {
        public EmailSubscriptionController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        [HttpPost]
        public IActionResult Subscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                TempData["Error"] = "Please enter a valid email address.";
                return CurrentUmbracoPage();
            }

            // TODO: Add your email subscription logic here
            // Examples:
            // - Save to database
            // - Send to email marketing service (MailChimp, SendGrid, etc.)
            // - Send notification email

            // For now, just log it (you can check Umbraco logs)
            // Logger.LogInformation("Email subscription received: {Email}", email);

            TempData["Success"] = "Thank you for subscribing!";
            return CurrentUmbracoPage();
        }
    }
}
```

---

### Step 7: Add Content in Umbraco Backoffice

1. **Navigate to Content**

   - Go to **Content** → Your Service Page

2. **Add Block to Sections**

   - Scroll to the **Sections** property (BlockList)
   - Click **Add content**
   - Select **Email Subscription Form Section**

3. **Fill in the Content**

   - **Form Heading**: `How can we help you?`
   - **Form Description**: `online support 24/7, we are here to help you`

4. **Save and Publish**

---

## File Structure Summary

After implementation, you should have:

```
Onatrix_CMS/
├── Controllers/
│   └── Surface/
│       └── EmailSubscriptionController.cs                [NEW]
├── Views/
│   ├── Partials/
│   │   └── blocklist/
│   │       └── Components/
│   │           └── EmailSubscriptionFormSection.cshtml   [NEW]
│   ├── mainLayout.cshtml                                 [UPDATED - add CSS link]
│   └── ServicePage.cshtml                                [No changes needed]
├── wwwroot/
│   └── css/
│       └── services.css                                  [NEW or UPDATED]
└── uSync/v16/
    └── ContentTypes/
        └── emailsubscriptionformsection.config           [AUTO-GENERATED]
```

---

## Testing

1. **Build and Run**

   ```bash
   dotnet build
   dotnet run
   ```

2. **Navigate to Service Page**

   - Visit your Service Page in the browser

3. **Verify Layout**

   - Should see a centered dark gray form box
   - Heading: "How can we help you?"
   - Description: "online support 24/7, we are here to help you"
   - Email input with envelope icon on the right

4. **Test Form Submission**
   - Enter an email address
   - Click the envelope icon
   - Should stay on the same page (you can add success message display later)

---

## Optional Enhancements

### Display Success/Error Messages

Add this to `EmailSubscriptionFormSection.cshtml` after the opening `<section>` tag:

```cshtml
@if (TempData["Success"] != null)
{
    <div class="form-message form-success">
        @TempData["Success"]
    </div>
}

@if (TempData["Error"] != null)
{
    <div class="form-message form-error">
        @TempData["Error"]
    </div>
}
```

Add CSS for messages in `services.css`:

```css
.form-message {
  padding: 1rem;
  margin-bottom: 1rem;
  border-radius: 4px;
  text-align: center;
  font-size: 14px;
}

.form-success {
  background-color: #d4edda;
  color: #155724;
  border: 1px solid #c3e6cb;
}

.form-error {
  background-color: #f8d7da;
  color: #721c24;
  border: 1px solid #f5c6cb;
}
```

---

## Troubleshooting

### Issue: Block doesn't appear in Umbraco

- Make sure `IsElement` is checked when creating the Document Type
- Verify you added it to the correct BlockList Data Type
- Refresh the browser/clear cache

### Issue: Styles not applying

- Check that CSS file is linked in layout
- Clear browser cache (Ctrl+Shift+R or Cmd+Shift+R)
- Verify file path is correct

### Issue: Form not submitting

- Check Surface Controller namespace matches your project
- Verify form action path: `/umbraco/surface/emailsubscription/subscribe`
- Check browser console for errors

### Issue: Generated model not found

- Rebuild the project: `dotnet build`
- Models are auto-generated in `umbraco/models/` folder
- Check `AboutUsFormSection.generated.cs` exists

---

## Alternative: Add as Widget to Individual Service Detail Pages

If you want to add the email subscription form as a **widget** to individual service detail pages (like Risk Management, Financial Advisor, etc.) instead of the main Service Page, follow these additional steps:

### Step A: Add to Service Detail Widgets BlockList

1. **Find the Service Detail Asides BlockList**

   - Go to **Settings** → **Data Types**
   - Find `Service Detail Asides List` (this is the BlockList for the Aside/Widgets section)

2. **Add Email Subscription Form**
   - Click to edit the Data Type
   - Under **Available Blocks**, click **Add**
   - Select `Email Subscription Form Section`
   - Click **Submit**
   - **Save** the Data Type

### Step B: Add Widget to Individual Service Pages

1. **Navigate to a Service Detail Page**

   - Go to **Content** → **Services** → Select a service (e.g., "Risk Management")

2. **Go to Aside Tab**

   - Click on the **Aside** tab in the content editor

3. **Add the Widget**

   - Under **Widgets**, click **Add content**
   - Select **Email Subscription Form Section**

4. **Configure the Form**

   - **Form Heading**: `How can we help you?`
   - **Form Description**: `online support 24/7, we are here to help you`

5. **Save and Publish**

6. **Repeat for Other Services**
   - Repeat steps 1-5 for each service where you want the form to appear

### Step C: Verify Widget Placement

The widget will appear in the **left sidebar** (Aside section) of your Service Detail pages, as shown in your ServiceDetail.cshtml:

```cshtml
<aside class="w-1/4 flex flex-col gap-4">
    @await Html.GetBlockListHtmlAsync(Model.ServiceDetailAsides)
</aside>
```

### Notes on Widget Usage

- **Per-Service Control**: You can add the form to some services but not others
- **Flexible Positioning**: The form will appear in the order you arrange widgets
- **Reusable Content**: Same form component can be used in both locations
- **Sidebar Styling**: The form will automatically adapt to the sidebar width (w-1/4)

---

## Next Steps

1. Implement actual email subscription logic in `EmailSubscriptionController`
2. Add database storage for email addresses
3. Integrate with email marketing service
4. Add client-side validation with JavaScript
5. Add loading state to submit button
6. Implement GDPR-compliant consent checkbox

---

## Questions?

If you encounter issues, check:

- Umbraco logs: `umbraco/Logs/`
- Browser console for JavaScript errors
- Network tab for form submission errors

Good luck with your implementation! 🚀
