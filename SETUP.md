# Onatrix CMS - Setup Guide

This guide will help you get the Onatrix CMS up and running on your local machine.

## Prerequisites

Before you begin, ensure you have the following installed:

- **.NET 9.0 SDK** or later - [Download here](https://dotnet.microsoft.com/download)
- **Node.js** (v18 or later) and **npm** - [Download here](https://nodejs.org/)
- **Git** - [Download here](https://git-scm.com/)
- **IDE** (recommended):
  - JetBrains Rider, or
  - Visual Studio 2022, or
  - Visual Studio Code with C# extensions

## Quick Start

Follow these steps to get the application running:

### 1. Clone the Repository

```bash
git clone <repository-url>
cd Onatrix_CMS
```

### 2. Navigate to the Project Directory

```bash
cd Onatrix_CMS
```

### 3. Restore .NET Dependencies

```bash
dotnet restore
```

### 4. Install NPM Dependencies (for Tailwind CSS)

```bash
npm install
```

### 5. Build the Project

```bash
dotnet build
```

### 6. Run the Application

```bash
dotnet run
```

The application should now be running! Open your browser and navigate to:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

Or check the console output for the exact URLs.

## First Time Setup

### Umbraco Backoffice Access

The Umbraco database and content are included in the repository via uSync, so you should have all content ready to go.

**Default Umbraco admin credentials:**
- URL: https://localhost:5001/umbraco
- Check with your team lead for admin credentials, or set up a new admin user if prompted.

### Tailwind CSS Development

If you need to make CSS changes, Tailwind is configured. To watch for changes:

```bash
npm run watch
```

This will watch for changes in your input CSS and rebuild the output CSS file automatically.

For more details, see: [README_TAILWIND.md](./README_TAILWIND.md)

## Project Structure

```
Onatrix_CMS/
├── Views/              # Razor views and templates
├── wwwroot/           # Static files (CSS, images, JS)
│   ├── css/           # Compiled CSS files
│   ├── images/        # Image assets
│   └── media/         # Umbraco media files
├── umbraco/           # Umbraco data and configuration
│   ├── Data/          # SQLite database
│   └── models/        # Generated content models
├── uSync/             # Umbraco sync configuration and content
└── App_Plugins/       # Custom Umbraco plugins
```

## Troubleshooting

### Port Already in Use

If you get an error that the port is already in use, you can either:
- Stop the other application using that port, or
- Change the port in `Properties/launchSettings.json`

### Database Issues

If you encounter database issues:
1. The SQLite database is located at `umbraco/Data/Umbraco.sqlite.db`
2. It's included in the repository, so it should work out of the box
3. If you need to reset, delete the database file and restart the app

### uSync Import

If content doesn't appear:
1. Log into the Umbraco backoffice
2. Go to Settings → uSync
3. Click "Import" to import all content

### CSS Not Updating

If your Tailwind CSS changes aren't reflecting:
```bash
npm run build
```

Or run the watch command:
```bash
npm run watch
```

## Development Workflow

1. Make your changes in the Views, CSS, or backend code
2. The application will hot-reload for most changes
3. For database/content changes, use uSync to sync changes
4. Commit your changes with meaningful commit messages

## Need Help?

- Check the main [README.md](./README.md) for project overview
- Review the Umbraco documentation: https://docs.umbraco.com/
- Contact your team lead for access credentials or project-specific questions

## Additional Resources

- [Tailwind CSS Setup](./README_TAILWIND.md)
- [Umbraco Documentation](https://docs.umbraco.com/)
- [.NET Documentation](https://docs.microsoft.com/dotnet/)

