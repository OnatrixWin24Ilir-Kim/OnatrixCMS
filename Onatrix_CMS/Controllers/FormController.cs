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
    FormSubmissionsServices formSubmissionsServices,
    FormSubmissionEmailService formSubmissionEmailService,
    FormSubmissionQuestionService formSubmissionQuestionService
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
    // Contact Form and Reqeust a call back Form
    [HttpPost]
    public IActionResult HandleCallBackForm(CallbackFormViewModel callbackFormViewModel)
    {
        // If not valid, send user back to Umbraco page with an error
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = formSubmissionsServices.SaveCallBackRequest(callbackFormViewModel);

        if (!result)
        {
            TempData["FormError"] = "Error, something went wrong, buy me an Pizza";
            return CurrentUmbracoPage();
        }

        TempData["FormSuccess"] = "Thank you for buying the Pizza";
        return RedirectToCurrentUmbracoPage();
    }

    // Email Form
    public IActionResult HandleEmailForm(CallBackFormEmailViewModel callBackFormEmailViewModel)
    {
        // If not valid, send user back to Umbraco page with an error
        if (!ModelState.IsValid)
        {

            return CurrentUmbracoPage();
        }

        var result = formSubmissionEmailService.SaveCallBackRequest(callBackFormEmailViewModel);

        if (!result)
        {
            TempData["FormErrorEmail"] = "Error, something went wrong, Buy me Pepsimax";
            return CurrentUmbracoPage();
        }

        TempData["FormSuccessEmail"] = "Thank you for all the data, I got you now!! MUHAHAHHA";
        return RedirectToCurrentUmbracoPage();
    }

    // Have a question form? Service Form?
    [HttpPost]
    public IActionResult HandleQuestionForm(CallBackFormQuestionServiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["FormErrorQuestion"] = "Please fill out all the required fields.";
            return CurrentUmbracoPage();
        }

        if (formSubmissionQuestionService.SaveCallBackRequest(model))
        {
            TempData["FormSuccessQuestion"] = "Your question has been submitted successfully!. I LOVE YOU!";
        }
        else
        {
            TempData["FormErrorQuestion"] = "Something went wrong, Ask chat GPT :)";
        }

        return RedirectToCurrentUmbracoPage();
    }
}
