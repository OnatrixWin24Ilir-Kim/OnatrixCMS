# Guide: Adding "Trusted by Biggest Names" Review Section

This guide will help you create the review/testimonial section that appears on the About page, featuring review content on the left and an image on the right.

## Overview

You'll create:
1. A **Block List Element Type** (Review Section) for the testimonial content
2. Add it to the **Onatrix - Sections** block list
3. A **Partial View** to render the review section
4. Add content in Umbraco

---

## 🚀 Quick Action Steps

**Step 1: Create Review Section Element Type**
1. Go to **Settings** → **Document Types** → **Compositions** → **Items**
2. Right-click on **Items** → **Create** → **Element Type**
3. Name it "Review Section"

**Step 2: Add to Onatrix - Sections Block List**
1. Go to **Settings** → **Data Types** → **Onatrix - Sections**
2. Add the Review Section block

**Step 3: Create the Partial View**
1. Create `ReviewSection.cshtml` in Views/Partials/blocklist/Components
2. Add the provided code

**Step 4: Add Content in Umbraco**
1. Add to About Page sections
2. Upload review image

---

## Step 1: Create the Review Section Element Type

### In Umbraco Backoffice:

1. Click **Settings** (in the left sidebar)
2. Navigate to **Document Types** → **Compositions** → **Items**
3. Right-click on **Items** → **Create** → **Element Type**
4. Set the following:
   - **Name**: `Review Section`
   - **Alias**: Will auto-generate as `reviewSection`
   - **Icon**: Choose `icon-chat` or `icon-quote-alt`
   - **Description**: `Customer review/testimonial section with image`

5. Click **Add Group** and name it `Content`

6. Add the following properties:

   **Property 1: Section Label**
   - **Name**: `Section Label`
   - **Alias**: `sectionLabel`
   - **Data Type**: `Textstring`
   - **Description**: `Small text above heading (e.g., "GREAT REVIEWS FOR OUR SERVICES")`
   - **Mandatory**: ✗ (unchecked)

   **Property 2: Heading**
   - **Name**: `Heading`
   - **Alias**: `heading`
   - **Data Type**: `Textstring`
   - **Description**: `Main heading (e.g., "Trusted by some Biggest Names")`
   - **Mandatory**: ✓ (checked)

   **Property 3: Description**
   - **Name**: `Description`
   - **Alias**: `description`
   - **Data Type**: `Textarea`
   - **Description**: `Paragraph text below heading`
   - **Mandatory**: ✗ (unchecked)

   **Property 4: Star Rating**
   - **Name**: `Star Rating`
   - **Alias**: `starRating`
   - **Data Type**: `Numeric`
   - **Description**: `Number of stars (1-5)`
   - **Mandatory**: ✗ (unchecked)

   **Property 5: Reviewer Name**
   - **Name**: `Reviewer Name`
   - **Alias**: `reviewerName`
   - **Data Type**: `Textstring`
   - **Description**: `Name of the person (e.g., "Kevin Gardner")`
   - **Mandatory**: ✗ (unchecked)

   **Property 6: Company Name**
   - **Name**: `Company Name`
   - **Alias**: `companyName`
   - **Data Type**: `Textstring`
   - **Description**: `Company name (e.g., "Swebank")`
   - **Mandatory**: ✗ (unchecked)

   **Property 7: Review Image**
   - **Name**: `Review Image`
   - **Alias**: `reviewImage`
   - **Data Type**: `Media Picker`
   - **Description**: `Image of the reviewer (recommended 600x800px)`
   - **Mandatory**: ✗ (unchecked)

7. Click **Save**

**Result:** You should now see "Review Section" in your Items folder.

---

## Step 2: Add Review Section to Onatrix - Sections Block List

### In Umbraco Backoffice:

1. Click **Settings** (in the left sidebar)
2. Click **Data Types**
3. **Find and click on:** `Onatrix - Sections`
4. Scroll down to **"Available Blocks"**
5. Click the **Add** button
6. Select **Review Section**
7. **Set the label template** to: `{{heading}}`
   - This shows the heading in the block list
8. Click **Submit**
9. Click **Save**

✅ **Done!** Review Section is now available in your sections.

---

## Step 3: Create the Partial View

### File to Create:

**File**: `/Views/Partials/blocklist/Components/ReviewSection.cshtml`

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<Umbraco.Cms.Core.Models.Blocks.BlockListItem>

@{
    var content = (ReviewSection)Model.Content;
    var sectionLabel = content.SectionLabel ?? "";
    var heading = content.Heading ?? "";
    var description = content.Description ?? "";
    var starRating = content.StarRating;
    var reviewerName = content.ReviewerName ?? "";
    var companyName = content.CompanyName ?? "";
    var reviewImage = content.ReviewImage?.Url() ?? "";
}

<section class="review-section py-16 md:py-20 bg-[#f7f7f7]">
    <div class="max-w-7xl mx-auto px-4">
        <div style="display: flex; flex-direction: row; gap: 3rem; align-items: center;">
            
            <!-- Review Content - Left Side -->
            <div style="flex: 1;">
                @if (!string.IsNullOrEmpty(sectionLabel))
                {
                    <p class="text-secondary uppercase tracking-wider mb-4" style="font-size: 14px; font-weight: 400;">
                        @sectionLabel
                    </p>
                }

                @if (!string.IsNullOrEmpty(heading))
                {
                    <h2 class="text-[#535656] font-bold mb-6" style="font-size: 40px; line-height: 1.2;">
                        @heading
                    </h2>
                }

                @if (!string.IsNullOrEmpty(description))
                {
                    <p class="text-[#535656] mb-6" style="font-size: 16px; line-height: 1.8; font-style: italic;">
                        @description
                    </p>
                }

                <!-- Star Rating -->
                @if (starRating > 0)
                {
                    <div class="flex gap-1 mb-6">
                        @for (int i = 0; i < starRating; i++)
                        {
                            <svg width="16" height="16" fill="#FFD700" viewBox="0 0 20 20">
                                <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"/>
                            </svg>
                        }
                    </div>
                }

                <!-- Reviewer Info -->
                @if (!string.IsNullOrEmpty(reviewerName))
                {
                    <p class="text-[#535656] font-semibold" style="font-size: 16px; margin-bottom: 0.25rem;">
                        @reviewerName
                    </p>
                }

                @if (!string.IsNullOrEmpty(companyName))
                {
                    <p class="text-[#535656] italic" style="font-size: 14px;">
                        @companyName
                    </p>
                }
            </div>

            <!-- Review Image - Right Side -->
            @if (!string.IsNullOrEmpty(reviewImage))
            {
                <div style="flex: 0 0 45%; max-width: 600px;">
                    <img src="@reviewImage" 
                         alt="@reviewerName" 
                         class="w-full h-auto object-cover" />
                </div>
            }
        </div>
    </div>
</section>
```

---

## Step 4: Rebuild Tailwind CSS

### In Terminal:

```bash
cd /Users/kimhammerstad/RiderProjects/Onatrix_CMS/Onatrix_CMS
npm run build:css
```

---

## Step 5: Add Content in Umbraco

### In Umbraco Backoffice:

1. Go to **Content** → **About** (your About page)
2. Find the **Sections** field (block list)
3. Click **Add content**
4. Select **Review Section**
5. Fill in the fields:

   **Section Label:**
   ```
   GREAT REVIEWS FOR OUR SERVICES
   ```

   **Heading:**
   ```
   Trusted by some Biggest Names
   ```

   **Description:**
   ```
   Seamlessly visualize quality intellectual capital without superior collaboration and idea-sharing. Holistically pontificate installed base portals.
   ```

   **Star Rating:**
   ```
   5
   ```

   **Reviewer Name:**
   ```
   Kevin Gardner
   ```

   **Company Name:**
   ```
   Swebank
   ```

   **Review Image:**
   - Upload the image of the guy working on laptop
   - Recommended size: 600x800px or similar landscape orientation

6. Click **Submit**
7. Click **Save and Publish**

---

## Step 6: Upload Review Image

### In Umbraco Media:

1. Go to **Media** section
2. Create a folder called **"Reviews"** or **"Testimonials"** (if it doesn't exist)
3. Upload the review image:
   - Professional photo of person
   - Good lighting
   - Business/office setting
   - Recommended: 600x800px or similar
   - Format: JPG or PNG

---

## Expected File Structure

```
Onatrix_CMS/
├── Views/
│   └── Partials/
│       └── blocklist/
│           └── Components/
│               ├── ReviewSection.cshtml     ← NEW FILE
│               ├── CeoQuote.cshtml          (existing)
│               └── ...
└── wwwroot/
    └── css/
        └── styles.css                        (rebuilt)
```

---

## Design Features

### Layout:
- **Two-column**: Content on left (55%), image on right (45%)
- **Responsive**: Stacks vertically on mobile, side-by-side on desktop
- **Background**: Light gray (#f7f7f7) for subtle contrast
- **Centered alignment**: Items center-aligned vertically

### Typography:
- **Label**: 14px, uppercase, secondary color, tracking-wider
- **Heading**: 40px, bold, dark gray
- **Description**: 16px, italic, comfortable line-height
- **Name**: 16px, semi-bold
- **Company**: 14px, italic

### Styling:
- **Star rating**: Gold stars (#FFD700)
- **Spacing**: Generous padding and gaps
- **Image**: Right-aligned, max 600px width

---

## Customization Options

### Change Background Color

```cshtml
<section class="review-section py-16 md:py-20 bg-white">      <!-- White -->
<section class="review-section py-16 md:py-20 bg-[#f7f7f7]"> <!-- Light gray -->
```

### Change Layout Proportions

```cshtml
<!-- More space for content -->
<div style="flex: 1;">  <!-- Content: 60% -->
<div style="flex: 0 0 40%; max-width: 500px;"> <!-- Image: 40% -->

<!-- More space for image -->
<div style="flex: 0 0 60%;">  <!-- Content: 40% -->
<div style="flex: 1; max-width: 700px;"> <!-- Image: 60% -->
```

### Change Star Color

```cshtml
<svg width="16" height="16" fill="#d9c3a9" viewBox="0 0 20 20"> <!-- Your brand color -->
<svg width="16" height="16" fill="#FFD700" viewBox="0 0 20 20"> <!-- Gold -->
```

### Change Heading Size

```cshtml
style="font-size: 48px; line-height: 1.2;"  <!-- Larger -->
style="font-size: 32px; line-height: 1.3;"  <!-- Smaller -->
```

---

## Troubleshooting

### "ReviewSection model not found" error:

1. Save and publish content in Umbraco
2. Rebuild your project in Rider (Build → Rebuild Solution)
3. Umbraco will auto-generate: `umbraco/models/ReviewSection.generated.cs`

### Section not showing:

1. Check that Review Section is added to Onatrix - Sections data type
2. Verify content is published in About page
3. Clear browser cache (Cmd+Shift+R)
4. Check console for errors (F12 → Console)

### Image not displaying:

1. Verify image is uploaded in Media
2. Check that image is selected in content
3. Check browser console for 404 errors

### Stars not showing:

1. Check that Star Rating is a number between 1-5
2. Verify SVG code is intact
3. Inspect element to see if stars are rendered

### Layout stacked instead of side-by-side:

1. Check that inline styles are not being overridden
2. Verify `display: flex; flex-direction: row;` is present
3. Check browser width (should work on desktop sizes)

---

## Alternative: Without Image

If you want to show just the review content without an image:

```cshtml
<section class="review-section py-16 md:py-20 bg-[#f7f7f7]">
    <div class="max-w-4xl mx-auto px-4 text-center">
        <!-- Center all content -->
        @if (!string.IsNullOrEmpty(sectionLabel))
        {
            <p class="text-secondary uppercase tracking-wider mb-4" style="font-size: 14px;">
                @sectionLabel
            </p>
        }
        
        <!-- Rest of content here, centered -->
    </div>
</section>
```

---

## Summary Checklist

- [ ] Create Review Section Element Type in Items
- [ ] Add Review Section to Onatrix - Sections data type
- [ ] Create ReviewSection.cshtml partial view
- [ ] Rebuild Tailwind CSS
- [ ] Upload reviewer image to Media
- [ ] Add Review Section content to About Page
- [ ] Fill in all fields (label, heading, description, stars, name, company)
- [ ] Save and Publish
- [ ] Test on different screen sizes

---

## Content Guidelines

### Section Label:
- Keep it short and uppercase
- Example: "GREAT REVIEWS FOR OUR SERVICES", "CLIENT TESTIMONIALS", "WHAT THEY SAY"

### Heading:
- Catchy and trust-building
- 3-6 words maximum
- Examples: "Trusted by some Biggest Names", "Loved by Thousands", "Industry Leaders Choose Us"

### Description:
- The actual review/testimonial
- 1-2 sentences
- Should sound authentic and specific
- Avoid generic praise

### Star Rating:
- Typically 5 stars for testimonials
- Can use 4 stars for more realistic feel
- Don't go below 4 for displayed reviews

### Image:
- Professional photo
- Person should look happy/satisfied
- Business setting preferred
- Good lighting essential
- Landscape or square orientation works best

---

Good luck! 🚀

If you encounter any issues, check the Umbraco logs or browser console for error messages.

