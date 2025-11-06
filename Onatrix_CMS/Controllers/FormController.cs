using Microsoft.AspNetCore.Mvc;
using Onatrix_CMS.Services;
using Onatrix_CMS.ViewModels;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix_CMS.Controllers;

public class FormController(
    IUmbracoContextAccessor umbracoContextAccessor,
    IUmbracoDatabaseFactory databaseFactory,
    ServiceContext services,
    AppCaches appCaches,
    IProfilingLogger profilingLogger,
    IPublishedUrlProvider publishedUrlProvider,
    FormSubmissionsServices formSubmissionsServices
)
    : SurfaceController(
        umbracoContextAccessor,
        databaseFactory,
        services,
        appCaches,
        profilingLogger,
        publishedUrlProvider
    )
{
    // GET

    public IActionResult HandleCallBackForm(CallbackFormViewModel model)
    {
        // If not valid, send user back to Umbraco page with an error
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = formSubmissionsServices.SaveCallBackRequest(model);

        if (!result)
        {
            TempData["FormError"] = "Error, something went wrong, buy me an Pizza"; 
            return CurrentUmbracoPage();
        }
        
        
        TempData["FormSuccess"] = "Thank you for buying the Pizza"; 
        return RedirectToCurrentUmbracoPage();
    }
}
