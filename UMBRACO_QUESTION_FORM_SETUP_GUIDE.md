# Umbraco Question Form Setup Guide

## Overview
This guide explains how to set up the Question Form component in Umbraco so it can be managed through the backoffice, allowing editors to add/remove it on Service Detail pages.

---

## Prerequisites
- Question Form Block Element Type already created
- CSS styling already in `service.css`
- `_QuestionForm.cshtml` partial view already exists

---

## Step 1: Create Block List Property in Service Detail Document Type

### In Umbraco Backoffice:

1. **Go to Settings** → **Document Types**
2. **Click on "Service Detail"** document type
3. **Make sure you're on the "Design" tab**
4. **Scroll down and find or create a new section** (e.g., "Content Blocks" or "Below Content")
5. **Click "Add property"**
6. **Configure the property:**
   - **Name**: `Content Blocks Below Body` (or similar)
   - **Alias**: `serviceDetailContentBlocks` (auto-generated)
   - **Property Editor**: Select **"Block List"**
   - **Description**: "Add content blocks that appear below the main body content"
7. **Click "Submit"**

---

## Step 2: Configure the Block List Property

1. **Click on the property you just created** to configure it
2. **In the "Settings" tab**, find **"Available Blocks"**
3. **Click "+ Add"** button
4. **Select "Question Form Block"** from the list
5. **Click "Submit"**
6. **Set "Amount"** (optional):
   - Minimum: `0`
   - Maximum: `∞` (or leave unlimited)
7. **Click "Save"** on the property configuration
8. **Click "Save"** on the Service Detail Document Type

---

## Step 3: Update ServiceDetail.cshtml View

Add the block list rendering in the view file (AFTER the main content section):

```cshtml
<section class="py-16">
    <div class="max-w-7xl mx-auto px-4">
        <div class="flex flex-row gap-8">
            <aside class="w-1/4 flex flex-col gap-4">
                @await Html.GetBlockListHtmlAsync(Model.ServiceDetailAsides)
            </aside>

            <div class="block w-px bg-gray-200"></div>

            <div class="flex-1 flex flex-col gap-2">
                @if (Model.ServiceDetailBody != null)
                {
                    <div class="service-detail-body">
                        @Html.Raw(Model.ServiceDetailBody)
                    </div>
                }
            </div>
        </div>
    </div>
</section>

@* Content Blocks Below Body - Can include Question Form *@
@if (Model.ServiceDetailContentBlocks != null && Model.ServiceDetailContentBlocks.Any())
{
    <section class="content-blocks-section">
        @await Html.GetBlockListHtmlAsync(Model.ServiceDetailContentBlocks)
    </section>
}

@* Contact Person Section *@
<partial name="Partials/Layout/_ContactPerson" />
```

**Note**: Replace `ServiceDetailContentBlocks` with the actual alias you created in Step 1.

---

## Step 4: Create Block List Component View (if not exists)

**File**: `Views/Partials/blocklist/Components/QuestionFormBlock.cshtml`

Make sure this file exists and contains:

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@using ContentModels = Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<Umbraco.Cms.Core.Models.Blocks.BlockListItem>

@{
    var content = (ContentModels.QuestionFormBlock)Model.Content;
    var formTitle = content.FormTitle ?? "Have a question ?";
    var showForm = content.ShowForm;
}

@if (showForm)
{
    <section class="question-form-section">
        <div class="question-form-container">
            <h3 class="question-form-title">@formTitle</h3>

            <form method="post" class="question-form">
                <div class="question-form-row">
                    <div class="question-form-field">
                        <input type="text"
                               name="Name"
                               class="question-form-input"
                               placeholder="Name"
                               required />
                    </div>

                    <div class="question-form-field">
                        <input type="email"
                               name="Email"
                               class="question-form-input"
                               placeholder="Email"
                               required />
                    </div>
                </div>

                <div class="question-form-field">
                    <textarea name="Question"
                              class="question-form-textarea"
                              placeholder="Question"
                              rows="6"
                              required></textarea>
                </div>

                <button type="submit" class="question-form-submit">
                    Submit
                </button>
            </form>
        </div>
    </section>
}
```

---

## Step 5: Add CSS Styling

**File**: `wwwroot/css/service.css`

Make sure the following CSS is present:

```css
/* ===================================
   Question Form Section
   =================================== */

.question-form-section {
  padding: 4rem 0;
  background-color: #ffffff;
  width: 100%;
  display: block;
  clear: both;
}

.question-form-container {
  max-width: 1280px;
  margin: 0 auto;
  padding: 2.5rem 4rem;
  background-color: #ffffff;
  box-sizing: border-box;
}

.question-form-title {
  font-size: 1.5rem;
  font-weight: 600;
  color: #535656;
  margin: 0 0 1.5rem 0;
  font-family: "Poppins", sans-serif;
}

.question-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.question-form-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
}

.question-form-field {
  display: flex;
  flex-direction: column;
}

.question-form-input,
.question-form-textarea {
  width: 100%;
  padding: 0.875rem 1rem;
  border: 1px solid #e5e5e5;
  border-radius: 4px;
  font-size: 0.875rem;
  font-family: "Poppins", sans-serif;
  color: #535656;
  background-color: #f9f9f9;
  transition: border-color 0.3s ease;
  box-sizing: border-box;
}

.question-form-input:focus,
.question-form-textarea:focus {
  outline: none;
  border-color: #d9c3a9;
  background-color: #ffffff;
}

.question-form-input::placeholder,
.question-form-textarea::placeholder {
  color: #999999;
  font-style: italic;
}

.question-form-textarea {
  resize: vertical;
  min-height: 120px;
}

.question-form-submit {
  background-color: #4f5955;
  color: #ffffff;
  padding: 0.875rem 2.5rem;
  border: none;
  border-radius: 4px;
  font-size: 0.9rem;
  font-weight: 600;
  font-family: "Poppins", sans-serif;
  cursor: pointer;
  transition: background-color 0.3s ease;
  width: fit-content;
  margin-top: 0.5rem;
}

.question-form-submit:hover {
  background-color: #3a3f3c;
}

.question-form-success {
  background-color: #d4edda;
  border: 1px solid #c3e6cb;
  color: #155724;
  padding: 0.875rem 1rem;
  border-radius: 4px;
  margin-bottom: 1rem;
  font-size: 0.875rem;
}

.question-form-error {
  background-color: #f8d7da;
  border: 1px solid #f5c6cb;
  color: #721c24;
  padding: 0.875rem 1rem;
  border-radius: 4px;
  margin-bottom: 1rem;
  font-size: 0.875rem;
}

.field-validation-error {
  color: #dc3545;
  font-size: 0.75rem;
  margin-top: 0.25rem;
  display: block;
}

/* Responsive Design */
@media (max-width: 1023px) {
  .question-form-container {
    padding: 2.5rem 2rem;
  }
}

@media (max-width: 768px) {
  .question-form-section {
    padding: 3rem 0;
  }

  .question-form-container {
    padding: 1.5rem 1rem;
  }

  .question-form-row {
    grid-template-columns: 1fr;
  }
}
```

---

## Step 6: Add Question Form to a Service Detail Page

### In Umbraco Backoffice:

1. **Go to Content** section
2. **Navigate to a Service Detail page** (e.g., "Risk Management")
3. **Scroll down** to find the new property you created (e.g., "Content Blocks Below Body")
4. **Click "Create new"** or the "+" button
5. **Select "Question Form Block"**
6. **Configure the block** (optional):
   - **Form Title**: Customize or leave default "Have a question ?"
   - **Show Form**: Toggle ON/OFF
7. **Click "Submit"**
8. **Save & Publish** the page

---

## Step 7: Verify the Setup

1. **Refresh the frontend** page
2. **Verify the form appears** below the main content
3. **Check that it's full-width** and centered
4. **Test responsive design** on mobile

---

## Troubleshooting

### Form doesn't appear:
- ✅ Check that the property alias matches in the view (`ServiceDetailContentBlocks`)
- ✅ Verify the block list is rendering: `@await Html.GetBlockListHtmlAsync(Model.ServiceDetailContentBlocks)`
- ✅ Check Umbraco logs for errors
- ✅ Verify the Question Form Block is added to the page content

### Form appears but not styled:
- ✅ Check that `service.css` is linked in `mainLayout.cshtml`
- ✅ Verify CSS classes match exactly (check for typos)
- ✅ Clear browser cache
- ✅ Check browser dev tools for CSS conflicts

### Form appears in wrong location:
- ✅ Verify the block list rendering is in the correct position in `ServiceDetail.cshtml`
- ✅ Check that it's outside the flex container (not in the sidebar)
- ✅ Ensure it's after the main content section

---

## Summary

After completing these steps:
- ✅ Question Form can be added/removed through Umbraco backoffice
- ✅ Form appears below main content, full-width
- ✅ Editors can customize form title and toggle visibility
- ✅ Form is reusable across all Service Detail pages
- ✅ Styling matches design requirements

---

**Created**: November 4, 2025  
**Version**: 1.0

