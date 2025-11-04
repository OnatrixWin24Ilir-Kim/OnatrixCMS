# Service Detail Question Form Implementation Guide

## Overview

This guide explains how to add the "Have a question?" form component to Service Detail pages in Umbraco. The form will appear in the sidebar and allows users to submit questions with their name, email, and question text.

**Note:** This is a simplified static form. ViewModel and Controller functionality can be added later for form processing.

---

## Design Reference

Based on the Figma design, the form should have:

- Title: "Have a question ?"
- Clean, minimal styling with light gray backgrounds
- Two fields on the first row: Name and Email (side by side)
- Large textarea for Question
- Dark gray Submit button
- Appears above the "Let us know about your next project" contact section

---

## Implementation Steps

### Step 1: Create Document Type in Umbraco Backoffice

1. Go to **Settings** > **Document Types**
2. Click on **Create** > **Document Type** (or right-click and select Create)
3. Configure the Document Type:
   - **Name**: `Question Form Block`
   - **Alias**: `questionFormBlock` (auto-generated)
   - **Icon**: Click on icon and search for `icon-edit` or `icon-chat`
   - **Is Element Type**: ✓ Check this box (important!)
4. Click **Save**

**Optional Properties** (you can add these later if you want editors to customize):
- Add Property: `formTitle` (Textstring) - Default: "Have a question ?"
- Add Property: `showForm` (Toggle) - Default: true

---

### Step 2: Create the Block List Component View

**File**: `Onatrix_CMS/Views/Partials/blocklist/Components/QuestionFormBlock.cshtml`

```cshtml
@using Umbraco.Cms.Web.Common.PublishedModels;
@inherits Umbraco.Cms.Web.Common.Views.UmbracoViewPage<Umbraco.Cms.Core.Models.Blocks.BlockListItem>

<div class="question-form-container">
    <h3 class="question-form-title">Have a question ?</h3>

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
```

---

### Step 3: Add to Service Detail Asides

1. Go to **Settings** > **Document Types** > **Service Detail**
2. Find the **Service Detail Asides** property
3. Click on the property to configure it
4. In the **Block List** configuration, click **Add** under "Available Blocks"
5. Select **Question Form Block** from the list
6. Click **Submit** and **Save** the Document Type

Now editors can add the Question Form to any Service Detail page's sidebar through the Umbraco backoffice!

---

### Step 4: Add CSS Styling

**File**: `Onatrix_CMS/wwwroot/css/question-form.css` (create new file)

```css
/* Question Form Styles */
.question-form-container {
  background-color: #ffffff;
  padding: 2.5rem 2rem;
  border-radius: 4px;
  margin-bottom: 2rem;
}

.question-form-title {
  font-size: 1.5rem;
  font-weight: 600;
  color: #535656;
  margin-bottom: 1.5rem;
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

/* Responsive */
@media (max-width: 768px) {
  .question-form-row {
    grid-template-columns: 1fr;
  }

  .question-form-container {
    padding: 1.5rem 1rem;
  }
}
```

---

### Step 5: Include CSS in Main Layout

Add this line to `Onatrix_CMS/Views/mainLayout.cshtml` in the `<head>` section:

```cshtml
<link rel="stylesheet" href="~/css/question-form.css">
```

---

### Step 6: Add the Form Block to a Service Detail Page

1. Go to **Content** section in Umbraco
2. Navigate to a **Service Detail** page
3. Find the **Service Detail Asides** section
4. Click **Add content** (+ button)
5. Select **Question Form Block**
6. Click **Submit** or **Save and Publish**

The form will now appear in the sidebar of that Service Detail page!

---

## Testing Checklist

- [ ] Form appears in Service Detail sidebar
- [ ] Form displays correctly with proper styling
- [ ] All fields show placeholder text
- [ ] HTML5 validation works (required fields)
- [ ] Responsive design works on mobile (fields stack properly)
- [ ] Form matches Figma design

---

## Troubleshooting

**Form doesn't appear:**

- Check that CSS file is linked in mainLayout.cshtml
- Verify the partial path is correct
- Check Umbraco logs for errors

**Styling doesn't match:**

- Ensure CSS file is loaded (check browser dev tools)
- Check for CSS conflicts with existing styles
- Verify Tailwind classes aren't overriding custom CSS

---

## Next Steps (To Implement Later)

1. **Create ViewModel** - Add `QuestionFormViewModel.cs` with validation attributes
2. **Create Surface Controller** - Add `QuestionFormSurfaceController.cs` to handle form submission
3. **Update Form View** - Replace static form with Umbraco form helper and model binding
4. **Add Email Functionality** - Send email notifications on form submission
5. **Add Database Storage** - Store form submissions for tracking
6. **Add reCAPTCHA** - Implement spam protection
7. **Add Success Messages** - Display confirmation after submission

---

**Component Created By:** AI Assistant  
**Date:** November 4, 2025  
**Version:** 1.0
