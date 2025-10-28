# Troubleshooting Guide - "Not Found" Error

If you're getting a "Not found" error after cloning the repository, follow these steps:

## Quick Fix Steps

### 1. Import uSync Content (MOST COMMON FIX)

Even though the database is included, you may need to import the uSync configuration:

1. Start the application with `dotnet run`
2. Navigate to the Umbraco backoffice: `https://localhost:5001/umbraco`
3. Log in with your credentials
4. Go to **Settings** (in left sidebar)
5. Click on **uSync** (at the bottom of Settings)
6. Click the **Import** button
7. Wait for the import to complete (you should see green checkmarks)
8. Go back to the home page and refresh

### 2. Publish Content

If the import didn't fix it, try publishing the content:

1. Go to **Content** section in Umbraco backoffice
2. You should see a **Home** page
3. Right-click on **Home**
4. Select **Publish with descendants**
5. Click **Publish** in the dialog

### 3. Check if Content Exists

1. In Umbraco backoffice, go to **Content**
2. If you DON'T see any content (empty):
   - Go to **Settings** → **uSync** → **Import**
   - This will import all content from the `uSync/` folder
3. If you DO see content but still get "Not found":
   - Make sure the Home page has a green checkmark (published)
   - If it has a gray icon, right-click and publish it

### 4. Clear Cache and Restart

Sometimes a simple restart helps:

1. Stop the application (Ctrl+C in terminal)
2. Delete the TEMP folder:
   ```bash
   rm -rf Onatrix_CMS/umbraco/Data/TEMP/*
   ```
3. Restart the application:
   ```bash
   dotnet run
   ```

### 5. Check Database File

Make sure the database file exists:

```bash
ls -la Onatrix_CMS/umbraco/Data/Umbraco.sqlite.db
```

If it doesn't exist or is very small (less than 100KB), you may need to:
1. Pull the latest changes: `git pull`
2. Verify the database was downloaded correctly

### 6. Verify Template Exists

1. In Umbraco backoffice, go to **Settings** → **Templates**
2. Check if **HomePage** template exists
3. If templates are missing:
   - Go to **Settings** → **uSync** → **Import**

### 7. Check Application URL

Make sure you're accessing the correct URL:

- ✅ Try: `https://localhost:5001` or `http://localhost:5000`
- ✅ Check the console output for the actual port numbers
- ❌ Don't try to access subpages directly if the home page isn't working

### 8. Build the Project Fresh

If nothing else works:

```bash
# Clean the build
dotnet clean

# Restore packages
dotnet restore

# Rebuild
dotnet build

# Run
dotnet run
```

## Still Not Working?

### Check for These Common Issues:

1. **Port already in use**: Change port in `Properties/launchSettings.json`
2. **Missing npm packages**: Run `npm install` in the `Onatrix_CMS` folder
3. **Database permissions**: Make sure you have read/write permissions on the database file
4. **Antivirus blocking**: Some antivirus software blocks SQLite databases

### Get More Information

Check the logs for errors:

```bash
tail -f Onatrix_CMS/umbraco/Logs/UmbracoTraceLog.*.json
```

Look for error messages that might give you more clues.

## Complete Reset (Last Resort)

If nothing works, try a complete fresh start:

```bash
# Delete the TEMP folder
rm -rf Onatrix_CMS/umbraco/Data/TEMP

# Delete node_modules
rm -rf Onatrix_CMS/node_modules

# Delete bin and obj
rm -rf Onatrix_CMS/bin
rm -rf Onatrix_CMS/obj

# Reinstall everything
cd Onatrix_CMS
npm install
dotnet restore
dotnet build
dotnet run
```

Then follow steps 1 and 2 from the Quick Fix section above.

---

## Expected Behavior

When everything is working correctly:

1. Navigate to `https://localhost:5001`
2. You should see the Onatrix homepage with:
   - Hero section with "We bring success to you"
   - Services section
   - Success story section
   - All images should be visible

3. Backoffice at `https://localhost:5001/umbraco` should show:
   - Content tree with Home, About, Contact, Services pages
   - All templates under Settings
   - All media files under Media section

---

**Need help?** Contact your team lead or check the main [SETUP.md](./SETUP.md) guide.

