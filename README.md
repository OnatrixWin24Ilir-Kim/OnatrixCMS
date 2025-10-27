# Onatrix CMS

A modern Content Management System built with **Umbraco CMS** and styled with **Tailwind CSS**.

## 🚀 Quick Start

**New to the project? Start here:**

```bash
git clone <repository-url>
cd Onatrix_CMS/Onatrix_CMS
dotnet restore
npm install
dotnet run
```

Then open https://localhost:5001 in your browser.

**For detailed setup instructions, see [SETUP.md](./SETUP.md)**

## 📋 What's Included

This repository includes everything you need to run the application:

- ✅ **Full Umbraco configuration** with uSync
- ✅ **Database with sample content** (SQLite)
- ✅ **All media files** and assets
- ✅ **Templates and views**
- ✅ **Tailwind CSS** configuration and compiled styles
- ✅ **Custom components** and block grid layouts

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core 9.0
- **CMS**: Umbraco CMS
- **Styling**: Tailwind CSS
- **Database**: SQLite (included)
- **Sync**: uSync for content versioning

## 📁 Project Structure

```
Onatrix_CMS/
├── Views/              # Razor templates and partials
├── wwwroot/           # Static files (CSS, JS, images)
├── umbraco/           # Umbraco data, logs, and models
├── uSync/             # Content sync configuration
└── App_Plugins/       # Custom Umbraco plugins
```

## 🔧 Development

### Running the Application

```bash
cd Onatrix_CMS
dotnet run
```

### Working with Tailwind CSS

Watch for changes (development):
```bash
npm run watch
```

Build for production:
```bash
npm run build
```

See [README_TAILWIND.md](./Onatrix_CMS/README_TAILWIND.md) for details.

### Accessing Umbraco Backoffice

Navigate to: https://localhost:5001/umbraco

Contact your team lead for admin credentials.

## 📚 Documentation

- [SETUP.md](./SETUP.md) - Detailed setup instructions
- [README_TAILWIND.md](./Onatrix_CMS/README_TAILWIND.md) - Tailwind CSS configuration
- [Umbraco Documentation](https://docs.umbraco.com/)

## 🤝 Contributing

1. Create a feature branch from `main`
2. Make your changes
3. Test locally
4. Submit a pull request

## 📝 Notes

- The SQLite database is committed to the repository for easy setup
- Media files are included in `wwwroot/media/`
- Content is synced via uSync in the `uSync/` directory
- Build artifacts (`bin/`, `obj/`, `node_modules/`) are gitignored

## 🆘 Troubleshooting

Having issues? Check the [SETUP.md](./SETUP.md) troubleshooting section or contact your team lead.

---

**Happy coding! 🎉**
