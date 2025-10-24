# Tailwind CSS Setup

This project uses **Tailwind CSS v4.1.16** for styling.

## Installation

The dependencies are already configured in `package.json`. To install them, run:

```bash
npm install
```

## Building CSS

### Production Build

To build the CSS once:

```bash
npm run build:css
```

This compiles `wwwroot/css/input.css` → `wwwroot/css/styles.css`

### Development Watch Mode

To watch for changes and rebuild automatically:

```bash
npm run watch:css
```

This is useful during development - it will rebuild the CSS every time you modify `input.css` or any file that contains Tailwind classes.

## Custom Theme

Custom colors and fonts are defined in `wwwroot/css/input.css` using Tailwind v4's `@theme` directive:

- **Colors**: `primary`, `secondary`, `tertiary`, `text`, `note`, `bg-grey`, `footer`
- **Font**: `Poppins` (loaded from Google Fonts)

### Using Custom Colors

In your HTML/Razor views, you can use the custom colors with Tailwind utilities:

```html
<div class="text-primary bg-secondary">Hello World</div>
<p class="text-text">This uses the custom text color</p>
```

Or with CSS variables:

```css
.my-class {
  color: var(--color-primary);
  background-color: var(--color-secondary);
}
```

## Files

- `wwwroot/css/input.css` - Source file with Tailwind imports and custom styles
- `wwwroot/css/styles.css` - Generated output file (included in `mainLayout.cshtml`)
- `package.json` - NPM configuration with build scripts
- `node_modules/` - Dependencies (gitignored)
