# Guide: Adding CEO Quote Section to About Page

This guide will help you create the CEO/founder quote section that appears at the top of the About Us page, featuring an image and a quote with body text.

## Overview

You'll create:

1. A **Block List Element Type** (CEO Quote) for the quote content
2. A **Data Type** for the block list
3. A **Partial View** to render the CEO quote section
4. Update the **About Page** to use block list sections

---

## 🚀 Quick Action Steps

### What You Need to Create

**Step 1: Create CEO Quote Block Element Type**

1. Go to **Settings** → **Document Types** → **Compositions** → **Items**
2. Right-click on **Items** → **Create** → **Element Type**
3. Follow detailed instructions in "Step 1" below

**Step 2: Create the Partial View**

1. Create `CeoQuote.cshtml` in Views/Partials/blocklist/Components
2. Add the provided code
3. Follow detailed instructions in "Step 2" below

**Step 3: Add to About Page Sections**

1. The quote will be part of the About Page sections block list
2. Add content in Umbraco
3. Follow detailed instructions in "Step 3" below

---

## Step 1: Create the CEO Quote Element Type

### In Umbraco Backoffice:

1. Go to **Settings** → **Document Types**
2. Navigate to **Compositions** → **Items** folder
3. Right-click on **Items** → **Create** → **Element Type**
4. Set the following:

   - **Name**: `CEO Quote`
   - **Alias**: Will auto-generate as `ceoQuote`
   - **Icon**: Choose `icon-quote` or `icon-user-female`
   - **Description**: `CEO/Founder quote section with image and text`

5. Click **Add Group** and name it `Content`

6. Add the following properties:

   **Property 1: Quote Text**

   - **Name**: `Quote Text`
   - **Alias**: `quoteText`
   - **Data Type**: `Textarea`
   - **Description**: `The main quote text (e.g., "We have over 25 years...")`
   - **Mandatory**: ✓ (checked)

   **Property 2: CEO Image**

   - **Name**: `CEO Image`
   - **Alias**: `ceoImage`
   - **Data Type**: `Media Picker`
   - **Description**: `Image of CEO/founder (recommended 600x800px)`
   - **Mandatory**: ✓ (checked)

   **Property 3: CEO Name**

   - **Name**: `CEO Name`
   - **Alias**: `ceoName`
   - **Data Type**: `Textstring`
   - **Description**: `Name and title (e.g., "Tina Fox - CEO & Founder at Onatrix")`
   - **Mandatory**: ✗ (unchecked)

   **Property 4: Body Content**

   - **Name**: `Body Content`
   - **Alias**: `bodyContent`
   - **Data Type**: `Richtext Editor` (or your custom RTE)
   - **Description**: `Main body paragraphs below the quote`
   - **Mandatory**: ✗ (unchecked)

7. Click **Save**

**Result:** You should now see "CEO Quote" in your Items folder.

---

## Step 2: Add CEO Quote to Sections Block List

### In Umbraco Backoffice:

**Goal:** Make CEO Quote available in the About Page sections

1. Click **Settings** (in the left sidebar)
2. Click **Data Types**
3. **Find and click on:** `Onatrix - Sections`
   - This is the block list that your About Page uses
4. Scroll down to the section that says **"Available Blocks"**
5. Click the **Add** button (or **Add content block**)
6. In the popup, find and select **CEO Quote**
7. **Set the label template** to: `{{quoteText}}`
   - This makes the quote text visible in the list when editing
8. Click **Submit** to close the popup
9. Click **Save** at the top right

✅ **Done!** CEO Quote is now available when editing About Page sections.

**Screenshot reference:** You should see "CEO Quote" added alongside your other blocks (like Hero Section, Team Section, etc.) in the Available Blocks list.

---

## Step 3: Create the Partial View

### File to Create:

**File**: `/Views/Partials/blocklist/Components/CeoQuote.cshtml`

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<Umbraco.Cms.Core.Models.Blocks.BlockListItem>

@{
    var content = (CeoQuote)Model.Content;
    var quoteText = content.QuoteText ?? "";
    var ceoImage = content.CeoImage?.Url() ?? "";
    var ceoName = content.CeoName ?? "";
    var bodyContent = content.BodyContent?.ToString() ?? "";
}

<section class="ceo-quote-section py-16 md:py-20 bg-white">
    <div class="max-w-7xl mx-auto px-4">
        <div class="flex flex-col md:flex-row gap-8 md:gap-12 items-start">

            <!-- CEO Image - Smaller, Left Side -->
            <div class="w-full md:w-1/3 lg:w-1/4 flex-shrink-0">
                @if (!string.IsNullOrEmpty(ceoImage))
                {
                    <div class="relative">
                        <img src="@ceoImage"
                             alt="@ceoName"
                             class="w-full h-auto object-cover"
                             style="max-width: 370px;" />

                        @if (!string.IsNullOrEmpty(ceoName))
                        {
                            <p class="mt-4 text-sm text-[#9c9ea6]">@ceoName</p>
                        }
                    </div>
                }
            </div>

            <!-- Quote and Body Content - Takes More Space -->
            <div class="w-full md:flex-1">
                @if (!string.IsNullOrEmpty(quoteText))
                {
                    <blockquote class="mb-6">
                        <p class="text-lg md:text-xl italic text-[#535656] leading-relaxed">
                            "@quoteText"
                        </p>
                    </blockquote>
                }

                @if (!string.IsNullOrEmpty(bodyContent))
                {
                    <div class="text-content text-[#535656] space-y-4 text-sm leading-relaxed">
                        @Html.Raw(bodyContent)
                    </div>
                }
            </div>
        </div>
    </div>
</section>

<style>
    /* Ensure paragraphs have proper spacing */
    .text-content p {
        margin-bottom: 1rem;
        line-height: 1.7;
    }

    .text-content p:last-child {
        margin-bottom: 0;
    }
</style>
```

---

## Step 4: Rebuild Tailwind CSS

### In Terminal:

Navigate to your project folder and run:

```bash
cd /Users/kimhammerstad/RiderProjects/Onatrix_CMS/Onatrix_CMS
npm run build:css
```

This ensures all the Tailwind classes used in the partial view are compiled.

---

## Step 5: Add Content in Umbraco

### In Umbraco Backoffice:

1. Go to **Content** → **About** (or your About page)
2. Find the **Sections** field (block list)
3. Click **Add content**
4. Select **CEO Quote**
5. Fill in the fields:

   **Quote Text:**

   ```
   We have over 25 years of experience providing expert financial advice to both businesses and individuals.
   ```

   **CEO Image:**

   - Upload or select an image of the CEO/founder
   - Recommended size: 600x800px portrait orientation
   - Professional photo with good lighting

   **CEO Name:**

   ```
   Tina Fox - CEO & Founder at Onatrix
   ```

   **Body Content:**

   - Add multiple paragraphs using the rich text editor
   - Use your custom RTE styles for formatting
   - Include "Lorem ipsum" or real content about the company

6. Click **Submit**
7. Click **Save and Publish**

---

## Step 6: Upload CEO Image

### In Umbraco Media:

1. Go to **Media** section
2. Create a folder called **"Team"** or **"About"** (if it doesn't exist)
3. Upload the CEO image:
   - Professional portrait
   - Good lighting
   - Business attire
   - Recommended: 600x800px (portrait orientation)
   - Format: JPG or PNG

---

## Expected File Structure

After completing this guide, you should have:

```
Onatrix_CMS/
├── Views/
│   └── Partials/
│       └── blocklist/
│           └── Components/
│               ├── CeoQuote.cshtml          ← NEW FILE
│               ├── BrandItem.cshtml          (existing)
│               ├── HeroSection.cshtml        (existing)
│               └── ...
└── wwwroot/
    └── css/
        └── styles.css                        (rebuilt)
```

---

## Design Features

The CEO quote section includes:

### Layout:

- **Side-by-side**: Image on left (40%), content on right (60%)
- **Responsive**: Stacks vertically on mobile, side-by-side on desktop
- **Proper spacing**: Generous padding and gaps between elements

### Typography:

- **Quote**: Large, italic, light font weight for emphasis
- **Body text**: Standard paragraph styling with good line height
- **Caption**: Smaller, subtle styling for the name/title

### Styling:

- **White background**: Clean, professional look
- **Shadow on image**: Subtle depth (optional)
- **Consistent colors**: Using your brand colors (#535656 for text)

---

## Customization Options

### Change Quote Styling

Modify the quote text size and style:

```cshtml
<!-- Larger quote -->
<p class="text-3xl md:text-4xl italic text-[#535656] leading-relaxed">

<!-- Different color -->
<p class="text-2xl md:text-3xl italic text-[#4f5955] leading-relaxed">
```

### Add Quote Icon

Add decorative quote marks:

```cshtml
<blockquote class="relative mb-10">
    <svg class="w-12 h-12 text-[#d9c3a9] opacity-30 mb-4" fill="currentColor" viewBox="0 0 32 32">
        <path d="M10 8v8h-8c0-4.4 3.6-8 8-8zM24 8v8h-8c0-4.4 3.6-8 8-8z"/>
    </svg>
    <p class="text-2xl md:text-3xl italic...">
        @quoteText
    </p>
</blockquote>
```

### Change Image Size

Adjust the max-width:

```cshtml
style="max-width: 350px;"  <!-- Smaller -->
style="max-width: 500px;"  <!-- Larger -->
```

### Add Background Color

Change section background:

```cshtml
<section class="ceo-quote-section py-20 bg-[#f7f7f7]">  <!-- Light gray -->
<section class="ceo-quote-section py-20 bg-white">       <!-- White -->
```

---

## Troubleshooting

### "CeoQuote model not found" error:

1. Save and publish content in Umbraco
2. Rebuild your project (Build → Rebuild Solution in Rider)
3. Umbraco will auto-generate: `umbraco/models/CeoQuote.generated.cs`

### Quote not showing:

1. Check that CEO Quote is added to the Sections block list data type
2. Verify content is published in About page
3. Clear browser cache (Cmd+Shift+R)
4. Check that the partial view path is correct

### Image not displaying:

1. Check that image is uploaded in Media
2. Verify the image is selected in the CEO Quote content
3. Check browser console for 404 errors (F12 → Console)

### Styling not working:

1. Rebuild Tailwind CSS: `npm run build:css`
2. Check that all Tailwind classes are valid
3. Clear browser cache
4. Inspect element in browser (F12) to see applied styles

### Content not rendering:

1. Check that `@Html.Raw(bodyContent)` is used for rich text
2. Verify the RTE has content
3. Check for any HTML/script errors in console

---

## Alternative: Simple Version (No Block List)

If you want a simpler approach without block lists, you can add fields directly to the About Page document type:

### Add to About Page Document Type:

1. Open **About Page** document type
2. Add the same 4 properties (Quote Text, CEO Image, CEO Name, Body Content)
3. Create partial view that reads from `Model` instead of `Model.Content`

**Simpler partial**:

```cshtml
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<AboutPage>

<section class="ceo-quote-section py-20 bg-white">
    <!-- Use Model.QuoteText, Model.CeoImage, etc. -->
</section>
```

---

## Summary Checklist

- [ ] Create CEO Quote Element Type in Compositions → Items
- [ ] Add CEO Quote to About Page Sections block list data type
- [ ] Create CeoQuote.cshtml partial view
- [ ] Rebuild Tailwind CSS
- [ ] Upload CEO image to Media
- [ ] Add CEO Quote content in About Page
- [ ] Add quote text and body content
- [ ] Save and Publish
- [ ] Test on different screen sizes
- [ ] Verify responsive layout works

---

## Content Guidelines

### Quote Text:

- Keep it concise and impactful
- 1-2 sentences maximum
- Should establish credibility or value proposition
- Example: "We have over 25 years of experience providing expert financial advice to both businesses and individuals."

### Body Content:

- 3-5 paragraphs
- Expand on the company story
- Include mission, values, expertise
- Keep paragraphs short and readable
- Use your custom RTE styles for emphasis

### CEO Image:

- Professional headshot or portrait
- Good lighting and background
- Business attire
- Friendly, approachable expression
- High quality (at least 600x800px)

---

Good luck! 🚀

If you encounter any issues, check the Umbraco logs at:
`/umbraco/Logs/UmbracoTraceLog.[ComputerName].[Date].json`
