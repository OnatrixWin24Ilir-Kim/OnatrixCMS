# Onatrix CMS

Onatrix CMS är ett modernt innehållshanteringssystem (Content Management System) utvecklat med Umbraco CMS och Tailwind CSS.  
Projektet är utformat för att erbjuda en flexibel, modulär och skalbar lösning för hantering av webbplatsinnehåll.

---

## Snabbstart

För att komma igång med projektet, följ stegen nedan:

```
git clone <repository-url>
cd Onatrix_CMS/Onatrix_CMS
dotnet restore
npm install
dotnet run
```

Därefter kan applikationen nås via:  
https://localhost:5001

För mer detaljerade installationsanvisningar, se SETUP.md.

---

## Innehåll

Detta repository innehåller samtliga komponenter som krävs för att köra applikationen:

- Fullständig Umbraco-konfiguration med uSync  
- Förkonfigurerad databas (SQLite) med exempeldata  
- Mediafiler och övriga resurser  
- Mallfiler och vyer  
- Tailwind CSS-konfiguration och kompilerade stilmallar  
- Anpassade komponenter och block grid-layouts  

---

## Teknisk översikt

- Backend: ASP.NET Core 9.0  
- CMS: Umbraco CMS  
- Stilmallar: Tailwind CSS  
- Databas: SQLite (inkluderad)  
- Innehållssynkronisering: uSync  

---

## Projektstruktur

```
Onatrix_CMS/
├── Views/             # Razor-vyer och partials
├── wwwroot/           # Statisk data (CSS, JavaScript, bilder)
├── umbraco/           # Umbraco-data, loggar och modeller
├── uSync/             # Konfiguration för innehållssynkronisering
└── App_Plugins/       # Anpassade Umbraco-pluginer
```

---

## Utveckling

### Köra applikationen

```
cd Onatrix_CMS
dotnet run
```

### Arbeta med Tailwind CSS

För utvecklingsläge (övervakning av förändringar):
```
npm run watch
```

För produktionsbygge:
```
npm run build
```

Mer information finns i README_TAILWIND.md.

---

### Åtkomst till Umbraco Backoffice

Administrationsgränssnittet nås via:  
https://localhost:5001/umbraco

Kontakta projektledare eller systemansvarig för administratörsinloggning.

---

## Dokumentation

- SETUP.md – Detaljerade installationsinstruktioner  
- README_TAILWIND.md – Konfiguration av Tailwind CSS  
- Umbraco Documentation – https://docs.umbraco.com/

---

## Bidra till projektet

För att bidra till projektets utveckling, följ dessa steg:

1. Skapa en ny gren från main  
2. Implementera dina ändringar  
3. Testa applikationen lokalt  
4. Skicka in en pull request  

---

## Övrig information

- SQLite-databasen är inkluderad i repositoryt för att underlätta installationen  
- Mediafiler finns under wwwroot/media/  
- Innehåll synkroniseras via uSync i katalogen uSync/  
- Kataloger som bin/, obj/ och node_modules/ är exkluderade via .gitignore  

---

## Felsökning

Om problem uppstår, se felsökningsavsnittet i SETUP.md eller kontakta ansvarig projektledare.

---

Onatrix CMS – Ett modernt och strukturerat innehållshanteringssystem byggt för framtidens webbmiljöer.
