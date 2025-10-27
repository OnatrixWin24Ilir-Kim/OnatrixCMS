# Guide: Adding Partner/Brand Logos Section

This guide will help you create a partner/brand logos section that displays above the footer (like FreedomBird, Identity, Natural, Simpleaf, Globe, FossilGroup).

## Overview

You'll create:

1. A new **Element Type** (Brand Item) for individual logos
2. A new **Data Type** (Brand List) for the block list
3. Add a field to **Site Settings** to manage the brands
4. A **Partial View** to render the brands
5. Update the **Main Layout** to display the section

---

## 🚀 Quick Action Steps

### Your Current Structure

Your Umbraco backoffice already has a well-organized structure with:

- ✅ **Compositions** → **Items** folder (where Social Media Item and Team member live)
- ✅ **Settings** → **Site Settings** (already configured)
- ✅ Good folder organization following Umbraco best practices

### What You Need to Add

**Step 1: Create Brand Item Element Type**

1. Right-click on **"Items"** folder (under Compositions → Items)
2. Click **Create** → **Element Type**
3. Follow the detailed instructions in "Step 1" below

**Step 2: Create Brand List Data Type**

1. Go to **Settings** → **Data Types**
2. Right-click on **Data Types** and select **Create** → **New Data Type**
3. Follow the detailed instructions in "Step 2" below

**Step 3: Update Site Settings**

1. Click on **Settings** → **Site Settings**
2. Add a new tab/group for "Partners" or "Brands"
3. Add the "Partner Brands" property using the Brand List data type you just created
4. Follow the detailed instructions in "Step 3" below

**Step 4-5: Code Files**

1. Create two new `.cshtml` files in your Views folder
2. Update `mainLayout.cshtml`
3. Rebuild Tailwind CSS

**Step 6-8: Add Content**

1. Upload brand logo images to Media
2. Add brand items in Site Settings
3. Publish and test

---

## Step 1: Create the Brand Item Element Type

### In Umbraco Backoffice:

1. Go to **Settings** → **Document Types**
2. Navigate to **Compositions** → **Items** folder
3. Right-click on **Items** folder → **Create** → **Element Type**
4. Set the following:
   - **Name**: `Brand Item`
   - **Alias**: Will auto-generate as `brandItem`
   - **Icon**: Choose `icon-picture` or `icon-plugin`
   - **Description**: `A single brand/partner logo`
5. Click **Add Group** and name it `Content`
6. Add the following properties:

   **Property 1:**

   - **Name**: `Brand Name`
   - **Alias**: `brandName`
   - **Data Type**: `Textstring`
   - **Description**: `Name of the brand/partner`
   - **Mandatory**: ✓ (checked)

   **Property 2:**

   - **Name**: `Brand Logo`
   - **Alias**: `brandLogo`
   - **Data Type**: `Media Picker`
   - **Description**: `Upload brand logo (PNG/SVG recommended)`
   - **Mandatory**: ✓ (checked)

   **Property 3:**

   - **Name**: `Brand URL`
   - **Alias**: `brandUrl`
   - **Data Type**: `Textstring`
   - **Description**: `Optional link to brand website`
   - **Mandatory**: ✗ (unchecked)

7. Click **Save**

**Result:** You should now see "Brand Item" in your Items folder, alongside Info Card, Social Media Item, and Team member.

---

## Step 2: Create the Brand List Data Type

### In Umbraco Backoffice:

1. Go to **Settings** → **Data Types**
2. Right-click on **Data Types** → **Create** → **New Data Type**
3. Set the following:
   - **Name**: `Brand List - Block List`
   - **Property Editor**: Select `Block List`
4. In the **Block List** configuration:
   - Click **Add** under "Available Blocks"
   - Select **Brand Item** (the element type you just created)
   - **Label**: Use `{{brandName}}` (this will show the brand name in the list)
   - Click **Submit**
5. Optional settings:
   - **Minimum**: Leave empty or set to 0
   - **Maximum**: Leave empty (unlimited) or set to 6-8
   - **Live editing mode**: Check this for better preview
6. Click **Save**

**Result:** You should now see "Brand List - Block List" in your Data Types section, similar to other block lists like "Team Member List".

---

## Step 3: Add Brand List to Site Settings

### In Umbraco Backoffice:

1. Go to **Settings** → **Document Types**
2. Find and open **Site Settings**
3. You should see existing tabs like "General", "Contact Information", "Social Media", "Footer"
4. Create a new tab or use existing one:
   - Click **Add Group**
   - **Group Name**: `Partners` or `Brands`
5. Click **Add Property** in that group:
   - **Name**: `Partner Brands`
   - **Alias**: `partnerBrands`
   - **Data Type**: Select the **Brand List - Block List** you just created
   - **Description**: `Add partner/brand logos to display above footer`
   - **Mandatory**: ✗ (unchecked)
6. Click **Save**

**Result:** Your Site Settings should now have a new "Partners" or "Brands" tab with the Partner Brands field where you can add logos.

---

## Step 4: Create the Partial View File

### Files to Create:

Create this file in your project:

**File**: `/Views/Partials/blocklist/Components/BrandItem.cshtml`

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<Umbraco.Cms.Core.Models.Blocks.BlockListItem>

@{
    var content = (BrandItem)Model.Content;
    var logoUrl = content.BrandLogo?.Url() ?? "";
    var brandName = content.BrandName ?? "Brand";
    var brandUrl = content.BrandUrl ?? "";
    var hasLink = !string.IsNullOrEmpty(brandUrl);
}

@if (hasLink)
{
    <a href="@brandUrl" target="_blank" rel="noopener noreferrer" class="flex items-center justify-center opacity-60 hover:opacity-100 transition-opacity duration-300">
        <img src="@logoUrl" alt="@brandName" class="h-12 w-auto object-contain grayscale hover:grayscale-0 transition-all duration-300" />
    </a>
}
else
{
    <div class="flex items-center justify-center opacity-60">
        <img src="@logoUrl" alt="@brandName" class="h-12 w-auto object-contain grayscale" />
    </div>
}
```

**File**: `/Views/Partials/Layout/_PartnerLogos.cshtml`

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage

@{
    var rootContent = Umbraco.ContentAtRoot();
    var siteSettings = rootContent.OfType<SiteSettings>().FirstOrDefault();
    var partnerBrands = siteSettings?.PartnerBrands;
}

@if (partnerBrands != null && partnerBrands.Any())
{
    <section class="partner-logos py-12 bg-gray-50">
        <div class="max-w-7xl mx-auto px-4">
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-8 items-center">
                @await Html.GetBlockListHtmlAsync(partnerBrands)
            </div>
        </div>
    </section>
}
```

---

## Step 5: Update Main Layout

### In your project:

Edit the file: `/Views/mainLayout.cshtml`

Find this section (around line 26-28):

```cshtml
@RenderBody()

<partial name="Partials/Layout/_Footer" />
```

**Change it to:**

```cshtml
@RenderBody()

<partial name="Partials/Layout/_PartnerLogos" />
<partial name="Partials/Layout/_Footer" />
```

---

## Step 6: Rebuild Tailwind CSS

### In Terminal:

Navigate to your project folder and run:

```bash
cd /Users/kimhammerstad/RiderProjects/Onatrix_CMS/Onatrix_CMS
npm run build:css
```

Or if you want to keep it running while developing:

```bash
npm run watch:css
```

---

## Step 7: Add Brand Logos in Umbraco

### In Umbraco Backoffice:

1. Go to **Content** → **Site Settings**
2. Find the **Partners** or **Brands** tab
3. Click **Add content** under "Partner Brands"
4. For each brand:
   - **Brand Name**: e.g., "FreedomBird"
   - **Brand Logo**: Click to upload or select from Media Library
   - **Brand URL**: (optional) e.g., "https://freedombird.com"
   - Click **Submit**
5. Repeat for all 6 brands (FreedomBird, Identity, Natural, Simpleaf, Globe, FossilGroup)
6. Click **Save and Publish**

---

## Step 8: Upload Brand Logos to Media

### In Umbraco Backoffice:

1. Go to **Media** section
2. Create a new folder called **"Partner Logos"** or **"Brands"**
3. Upload all 6 brand logo images (PNG or SVG format recommended)
4. Make sure they are grayscale or prepare to be converted to grayscale by CSS

---

## Expected File Structure

After completing this guide, you should have:

```
Onatrix_CMS/
├── Views/
│   ├── Partials/
│   │   ├── blocklist/
│   │   │   └── Components/
│   │   │       └── BrandItem.cshtml          ← NEW FILE
│   │   └── Layout/
│   │       ├── _PartnerLogos.cshtml          ← NEW FILE
│   │       └── _Footer.cshtml                 (existing)
│   └── mainLayout.cshtml                      (modified)
└── wwwroot/
    └── css/
        └── styles.css                          (rebuilt)
```

---

## Styling Notes

The provided CSS classes create:

- **Grid Layout**: 2 columns on mobile, 3 on tablet, 6 on desktop
- **Grayscale Effect**: Logos appear grayscale by default
- **Hover Effect**: Logos become colorful and more opaque on hover (if linked)
- **Responsive**: Adapts to different screen sizes
- **Background**: Light gray background (`bg-gray-50`) to separate from content

### Customize Styling

If you want to change the appearance, edit the classes in `_PartnerLogos.cshtml`:

- **Change grid columns**: Modify `grid-cols-2 md:grid-cols-3 lg:grid-cols-6`
- **Change logo height**: Modify `h-12` (12 = 3rem = 48px)
- **Remove grayscale**: Remove `grayscale` and `hover:grayscale-0` classes
- **Change background**: Modify `bg-gray-50` to another color or remove it
- **Adjust spacing**: Modify `gap-8` (gap between logos) or `py-12` (vertical padding)

---

## Troubleshooting

### "BrandItem model not found" error:

1. Save and publish content in Umbraco
2. Rebuild your project (Build → Rebuild Solution in Rider)
3. Umbraco will auto-generate the model file at: `umbraco/models/BrandItem.generated.cs`

### Logos not appearing:

1. Check that you've added logos in Site Settings
2. Check that media files are uploaded correctly
3. Clear browser cache (Cmd+Shift+R on Mac)
4. Check browser console for errors (F12 → Console)

### Styling not working:

1. Rebuild Tailwind CSS: `npm run build:css`
2. Clear browser cache
3. Check that `styles.css` is loaded in mainLayout.cshtml
4. Check browser console for CSS loading errors

### Wrong layout/spacing:

- Make sure you have the latest Tailwind CSS built
- Check that all Tailwind classes are valid
- Inspect element in browser (F12) to see applied styles

---

## Additional Enhancements (Optional)

### Add animation on scroll:

You could add a fade-in animation when the section comes into view using Alpine.js or vanilla JavaScript.

### Add carousel for mobile:

If you want logos to scroll horizontally on mobile instead of stacking, you can implement a carousel.

### Add different logo themes:

Store both light and dark versions of logos and swap based on theme.

---

## Summary Checklist

- [ ] Create Brand Item Element Type in Umbraco
- [ ] Create Brand List Data Type in Umbraco
- [ ] Add Partner Brands field to Site Settings
- [ ] Create BrandItem.cshtml partial view
- [ ] Create \_PartnerLogos.cshtml partial view
- [ ] Update mainLayout.cshtml to include partner logos
- [ ] Rebuild Tailwind CSS
- [ ] Upload brand logo images to Media
- [ ] Add brand items in Site Settings
- [ ] Test on different screen sizes
- [ ] Publish and verify on live site

---

Good luck! 🚀

If you encounter any issues, check the Umbraco logs at:
`/umbraco/Logs/UmbracoTraceLog.[ComputerName].[Date].json`
