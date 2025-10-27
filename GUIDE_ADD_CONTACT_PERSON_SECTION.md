# Guide: Adding Contact Person Section

This guide will help you create a reusable contact person section that displays on multiple pages (Services, About, Contact, and Service Detail pages).

## Overview

You'll create:

1. A **Contact Person Settings** section in Site Settings
2. A **Partial View** to render the contact person
3. Update page templates to include the section

---

## 🚀 Quick Action Steps

### What You Need to Add

**Step 1: Add Contact Person Fields to Site Settings**

1. Go to **Settings** → **Site Settings**
2. Add fields for contact person information
3. Follow detailed instructions in "Step 1" below

**Step 2: Create Contact Person Partial View**

1. Create `_ContactPerson.cshtml` in Views/Partials/Layout
2. Add the HTML/Razor code provided
3. Follow detailed instructions in "Step 2" below

**Step 3: Update Page Templates**

1. Add the partial to Services, About, Contact, and Service Detail pages
2. Follow detailed instructions in "Step 3" below

**Step 4: Add Content and Test**

1. Upload contact person image
2. Fill in contact details in Site Settings
3. Test on all pages

---

## Step 1: Add Contact Person Fields to Site Settings

### In Umbraco Backoffice:

1. Go to **Settings** → **Document Types**
2. Click on **Settings** folder → **Site Settings**
3. Click **Add Group** and name it `Contact Person` (or add to existing "Contact Information" tab)
4. Add the following properties:

   **Property 1: Contact Person Image**

   - **Name**: `Contact Person Image`
   - **Alias**: `contactPersonImage`
   - **Data Type**: `Media Picker`
   - **Description**: `Image of the contact person (recommended 800x800px)`
   - **Mandatory**: ✗ (unchecked)

   **Property 2: Contact Person Title**

   - **Name**: `Contact Person Title`
   - **Alias**: `contactPersonTitle`
   - **Data Type**: `Textstring`
   - **Description**: `Title/heading above contact details (e.g., "Let us know about your next project")`
   - **Mandatory**: ✗ (unchecked)

   **Property 3: Show Office Location**

   - **Name**: `Show Office Location`
   - **Alias**: `showOfficeLocation`
   - **Data Type**: `True/false`
   - **Description**: `Display office location in contact person section`
   - **Mandatory**: ✗ (unchecked)
   - **Default**: ✓ (checked/true)

   **Property 4: Show Phone Number**

   - **Name**: `Show Phone Number`
   - **Alias**: `showPhoneNumber`
   - **Data Type**: `True/false`
   - **Description**: `Display phone number in contact person section`
   - **Mandatory**: ✗ (unchecked)
   - **Default**: ✓ (checked/true)

   **Property 5: Show Email Address**

   - **Name**: `Show Email Address`
   - **Alias**: `showEmailAddress`
   - **Data Type**: `True/false`
   - **Description**: `Display email address in contact person section`
   - **Mandatory**: ✗ (unchecked)
   - **Default**: ✓ (checked/true)

5. Click **Save**

**Note:** You already have `contactAddress`, `contactPhone`, and `contactEmail` fields in Site Settings, so we'll reuse those!

---

## Step 2: Create the Contact Person Partial View

### File to Create:

**File**: `/Views/Partials/Layout/_ContactPerson.cshtml`

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage

@{
    var rootContent = Umbraco.ContentAtRoot();
    var siteSettings = rootContent.OfType<SiteSettings>().FirstOrDefault();

    var personImage = siteSettings?.ContactPersonImage?.Url() ?? "";
    var personTitle = siteSettings?.ContactPersonTitle ?? "Let us know about your next project";
    var showLocation = siteSettings?.ShowOfficeLocation ?? true;
    var showPhone = siteSettings?.ShowPhoneNumber ?? true;
    var showEmail = siteSettings?.ShowEmailAddress ?? true;

    var officeLocation = siteSettings?.ContactAddress ?? "";
    var phoneNumber = siteSettings?.ContactPhone ?? "";
    var emailAddress = siteSettings?.ContactEmail ?? "";

    // Only show section if we have at least the image
    var hasContent = !string.IsNullOrEmpty(personImage);
}

@if (hasContent)
{
    <section class="contact-person-section py-16 bg-gray-100">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex flex-col lg:flex-row items-center gap-8 lg:gap-16">

                <!-- Contact Person Image (with background and dots already included) -->
                <div class="w-full lg:w-1/2 flex justify-center lg:justify-start">
                    <img src="@personImage"
                         alt="Contact Person"
                         class="w-full max-w-md object-contain" />
                </div>

                <!-- Contact Information -->
                <div class="w-full lg:w-1/2 space-y-6">
                    <h2 class="text-3xl lg:text-4xl font-bold text-heading leading-tight">
                        @personTitle
                    </h2>

                    <div class="space-y-4">
                        @if (showLocation && !string.IsNullOrEmpty(officeLocation))
                        {
                            <div class="flex items-start gap-3">
                                <svg class="w-5 h-5 text-primary mt-1 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                                    <path fill-rule="evenodd" d="M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z" clip-rule="evenodd"/>
                                </svg>
                                <div>
                                    <p class="text-sm font-semibold text-heading">Office location</p>
                                    <p class="text-body-text">@officeLocation</p>
                                </div>
                            </div>
                        }

                        @if (showPhone && !string.IsNullOrEmpty(phoneNumber))
                        {
                            <div class="flex items-start gap-3">
                                <svg class="w-5 h-5 text-primary mt-1 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                                    <path d="M2 3a1 1 0 011-1h2.153a1 1 0 01.986.836l.74 4.435a1 1 0 01-.54 1.06l-1.548.773a11.037 11.037 0 006.105 6.105l.774-1.548a1 1 0 011.059-.54l4.435.74a1 1 0 01.836.986V17a1 1 0 01-1 1h-2C7.82 18 2 12.18 2 5V3z"/>
                                </svg>
                                <div>
                                    <p class="text-sm font-semibold text-heading">Phone number</p>
                                    <a href="tel:@phoneNumber.Replace(" ", "")" class="text-body-text hover:text-primary transition-colors">
                                        @phoneNumber
                                    </a>
                                </div>
                            </div>
                        }

                        @if (showEmail && !string.IsNullOrEmpty(emailAddress))
                        {
                            <div class="flex items-start gap-3">
                                <svg class="w-5 h-5 text-primary mt-1 flex-shrink-0" fill="currentColor" viewBox="0 0 20 20">
                                    <path d="M2.003 5.884L10 9.882l7.997-3.998A2 2 0 0016 4H4a2 2 0 00-1.997 1.884z"/>
                                    <path d="M18 8.118l-8 4-8-4V14a2 2 0 002 2h12a2 2 0 002-2V8.118z"/>
                                </svg>
                                <div>
                                    <p class="text-sm font-semibold text-heading">E-mail address</p>
                                    <a href="mailto:@emailAddress" class="text-body-text hover:text-primary transition-colors">
                                        @emailAddress
                                    </a>
                                </div>
                            </div>
                        }
                    </div>
                </div>
            </div>
        </div>
    </section>
}
```

---

## Step 3: Update Page Templates

You need to add the contact person section to 4 pages:

### 3.1 Services Page

**File**: `/Views/ServicePage.cshtml`

Add this line before the closing tag (or where you want the section):

```cshtml
<partial name="Partials/Layout/_ContactPerson" />
```

Example:

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<ServicePage>
@{
    Layout = "mainLayout.cshtml";
}

<partial name="Partials/Layout/_PageHeader" model="(IPageHeader)Model" />

<!-- Your services content here -->

<partial name="Partials/Layout/_ContactPerson" />
```

### 3.2 About Page

**File**: `/Views/AboutPage.cshtml`

Add the same line:

```cshtml
<partial name="Partials/Layout/_ContactPerson" />
```

### 3.3 Contact Page

**File**: `/Views/ContactPage.cshtml`

Add the same line:

```cshtml
<partial name="Partials/Layout/_ContactPerson" />
```

### 3.4 Service Detail Page

**File**: `/Views/ServiceDetail.cshtml`

Add the same line:

```cshtml
<partial name="Partials/Layout/_ContactPerson" />
```

---

## Step 4: Update Tailwind CSS Configuration

Make sure these colors are defined in your `/wwwroot/css/input.css`:

```css
@theme {
  --color-primary: #d9c3a9;
  --color-heading: #535656;
  --color-body-text: #9c9ea6;
}
```

---

## Step 5: Rebuild Tailwind CSS

### In Terminal:

Navigate to your project folder and run:

```bash
cd /Users/kimhammerstad/RiderProjects/Onatrix_CMS/Onatrix_CMS
npm run build:css
```

Or keep it running while developing:

```bash
npm run watch:css
```

---

## Step 6: Add Contact Person Content in Umbraco

### In Umbraco Backoffice:

1. Go to **Content** → **Site Settings**
2. Find the **Contact Person** tab (or Contact Information tab)
3. Fill in the following:

   **Contact Person Image:**

   - Click to upload the image of the contact person
   - Recommended size: 800x800px or similar portrait dimensions
   - The image should have a transparent or clean background

   **Contact Person Title:**

   - Enter: `Let us know about your next project`
   - Or customize as needed

   **Toggle Settings:**

   - ✓ **Show Office Location** (checked)
   - ✓ **Show Phone Number** (checked)
   - ✓ **Show Email Address** (checked)

4. The contact details (address, phone, email) should already be filled in from the existing Contact Information section

5. Click **Save and Publish**

---

## Step 7: Upload the Contact Person Image

### Option 1: Use the Existing Image (Recommended)

If you already have the merged image from Figma at `/wwwroot/images/backgrounds/guy-dot-background.png`:

1. Go to **Media** section in Umbraco
2. Create a folder called **"Contact"** (if it doesn't exist)
3. Upload `/wwwroot/images/backgrounds/guy-dot-background.png`
4. This image already includes:
   - The person photo
   - The decorative angular background
   - The dot pattern
5. Use this in Site Settings → Contact Person Image

### Option 2: Create Your Own from Figma

If you need to create a new merged image:

1. **In Figma:**

   - Select the person photo, background shape, and dot pattern
   - Right-click → **Flatten Selection** (or Cmd+E)
   - Right-click → **Export** → Choose **PNG**
   - Set scale to **2x** for retina displays
   - Export with transparency if needed

2. **Save the image:**

   - Name it descriptively (e.g., `contact-person.png`)
   - Save to `/wwwroot/images/backgrounds/` or upload via Umbraco Media

3. **In Umbraco Media:**
   - Go to **Media** section
   - Create a folder called **"Contact"** or **"Backgrounds"**
   - Upload your exported image

---

## Expected File Structure

After completing this guide, you should have:

```
Onatrix_CMS/
├── Views/
│   ├── Partials/
│   │   └── Layout/
│   │       ├── _ContactPerson.cshtml          ← NEW FILE
│   │       ├── _PartnerLogos.cshtml           (existing)
│   │       └── _Footer.cshtml                  (existing)
│   ├── AboutPage.cshtml                        (modified)
│   ├── ContactPage.cshtml                      (modified)
│   ├── ServicePage.cshtml                      (modified)
│   └── ServiceDetail.cshtml                    (modified)
└── wwwroot/
    └── css/
        ├── input.css                            (check colors)
        └── styles.css                           (rebuilt)
```

---

## Design Features

The contact person section includes:

### Visual Elements:

- **Responsive Layout**: Side-by-side on desktop, stacked on mobile
- **Pre-composed Image**: Person photo with decorative background and dots already merged (from Figma)
- **Professional Icons**: For location, phone, and email
- **Clean Implementation**: Simple, performant single-image approach

### Styling:

- **Gray Background**: `bg-gray-100` to separate from other sections
- **Proper Spacing**: Generous padding and gaps
- **Hover Effects**: Links change color on hover
- **Mobile Responsive**: Adapts to all screen sizes

### Flexibility:

- **Toggle Options**: Show/hide individual contact details
- **Reusable**: Same section appears on multiple pages
- **Manageable**: Update once in Site Settings, changes everywhere

---

## Customization Options

### Change Background Color

In `_ContactPerson.cshtml`, change:

```cshtml
<section class="contact-person-section py-16 bg-gray-100">
```

To:

```cshtml
<section class="contact-person-section py-16 bg-white">
<!-- or -->
<section class="contact-person-section py-16 bg-gray-50">
```

### Change Title Size

Modify the h2 classes:

```cshtml
<h2 class="text-3xl lg:text-4xl font-bold text-heading leading-tight">
<!-- Change to larger: -->
<h2 class="text-4xl lg:text-5xl font-bold text-heading leading-tight">
```

### Change Image Size

Modify the `max-w-md` class to adjust image size:

```cshtml
max-w-sm   <!-- Smaller (384px) -->
max-w-md   <!-- Medium (448px) - default -->
max-w-lg   <!-- Larger (512px) -->
max-w-xl   <!-- Extra large (576px) -->
```

### Change Section Padding

Modify `py-16` to increase/decrease vertical padding:

```cshtml
py-8   <!-- Less padding -->
py-12  <!-- Medium padding -->
py-20  <!-- More padding -->
```

---

## Troubleshooting

### Contact person not showing:

1. Check that you've uploaded an image in Site Settings
2. Verify the partial is included in the page template
3. Clear browser cache (Cmd+Shift+R on Mac)
4. Check Umbraco logs for errors

### Styling not working:

1. Rebuild Tailwind CSS: `npm run build:css`
2. Check that colors are defined in `input.css`
3. Clear browser cache
4. Inspect element in browser (F12) to see applied styles

### Icons not displaying:

1. The SVG icons are inline, so they should always work
2. Check browser console for errors (F12 → Console)
3. Verify the Tailwind classes are compiled

### Layout broken on mobile:

1. Check that you have responsive classes: `lg:flex-row`, `lg:w-1/2`
2. Test in browser dev tools responsive mode
3. Rebuild Tailwind CSS

### Wrong contact information showing:

1. Go to Site Settings → Contact Information
2. Verify the fields: `contactAddress`, `contactPhone`, `contactEmail`
3. Save and Publish
4. Clear cache and refresh

---

## Alternative: Show on Specific Pages Only

If you want the contact person to show ONLY on specific pages (like the partner logos), you can add this logic to `_ContactPerson.cshtml`:

Add after the variable declarations:

```cshtml
@{
    // ... existing variables ...

    // Only show on specific pages
    var showOnPages = new[] { "servicePage", "aboutPage", "contactPage", "serviceDetail" };
    var currentPageType = Model.ContentType.Alias;
    var shouldShowSection = showOnPages.Contains(currentPageType);

    // Only show section if we have content AND on correct page
    hasContent = hasContent && shouldShowSection;
}
```

---

## Summary Checklist

- [ ] Add Contact Person fields to Site Settings (image, title, toggles)
- [ ] Create `_ContactPerson.cshtml` partial view
- [ ] Update ServicePage.cshtml to include partial
- [ ] Update AboutPage.cshtml to include partial
- [ ] Update ContactPage.cshtml to include partial
- [ ] Update ServiceDetail.cshtml to include partial
- [ ] Check color variables in input.css
- [ ] Rebuild Tailwind CSS
- [ ] Upload contact person image to Media
- [ ] Fill in contact details in Site Settings
- [ ] Test on all 4 pages
- [ ] Test responsive design on mobile
- [ ] Publish and verify on live site

---

## Pages Where Contact Person Appears

1. ✅ **Services Page** (`/services`)
2. ✅ **About Page** (`/about`)
3. ✅ **Contact Page** (`/contact`)
4. ✅ **Service Detail Pages** (e.g., `/services/risk-management`)

---

Good luck! 🚀

If you encounter any issues, check the Umbraco logs at:
`/umbraco/Logs/UmbracoTraceLog.[ComputerName].[Date].json`
