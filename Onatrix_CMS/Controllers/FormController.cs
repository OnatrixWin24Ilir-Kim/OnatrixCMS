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
    FormSubmissionQuestionService formSubmissionQuestionService,
    UmbracoEmailService umbracoEmailService
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
    // Got support from Robin and Kim :)
    [HttpPost]
    public async Task<IActionResult> HandleCallBackForm(CallbackFormViewModel callbackFormViewModel)
    {
        // If not valid, send user back to Umbraco page with an error
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        // Save the callback request
        var result = formSubmissionsServices.SaveCallBackRequest(callbackFormViewModel);

        // If the request failed, log the error and return to the page with an error message
        if (!result)
        {
            TempData["FormError"] = "Error, something went wrong, buy me an Pizza";
            return CurrentUmbracoPage();
        }

        var name = callbackFormViewModel.Name;
        var selectedOption = callbackFormViewModel.SelectedOption;
        var htmlBody = $"""

                        <!DOCTYPE html>
                        <html>
                        <head>
                            <title>Callback Request Confirmation</title>
                        </head>
                        <body style="font-family: sans-serif; font-size: 16px; color: #333;">
                            <div style="max-width: 600px; margin: 20px auto; padding: 20px; border: 1px solid #ececec; border-radius: 5px;">
                                <h2 style="color: #4f5955;">Thank you for your callback request!</h2>
                                <p>Hello {name},</p>
                                <p>We have received your request ({selectedOption}) and will be in touch with you shortly. Your life belongs to us!</p>
                                <br>
                                <p>Best regards,<br>The Onatrix Team</p>
                            </div>
                        </body>
                        </html>
            """;

        // Send the email
        var emailResult = await umbracoEmailService.MailSendAsync(
            callbackFormViewModel.Email,
            $"Onatrix - Callback Request Confirmation",
            htmlBody,
            "$\"Thank you for your callback request, {name}! We will be in touch shortly.\""
        );

        // If the email failed, log the error and return to the page with an error message
        if (!emailResult)
        {
            TempData["FormErrorEmail"] = "Error, something went wrong, buy me Pepsimax";
            return CurrentUmbracoPage();
        }

        TempData["FormSuccess"] = "Thank you for buying the Pizza";

        return RedirectToCurrentUmbracoPage();
    }

    // Email Form
    public async Task<IActionResult> HandleEmailForm(
        CallBackFormEmailViewModel callBackFormEmailViewModel
    )
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
        // Html Body data
        var htmlBody = $"""

                        <!DOCTYPE html>
                        <html>
                        <head>
                            <title>Email Subscription Confirmation</title>
                        </head>
                        <body style="font-family: sans-serif; font-size: 16px; color: #333;">
                            <div style="max-width: 600px; margin: 20px auto; padding: 20px; border: 1px solid #ececec; border-radius: 5px;">
                                <h2 style="color: #4f5955;">Thank you for your email subscription!</h2>
                                <p>Hello {callBackFormEmailViewModel.SubscriptionEmail},</p>
                                <hr>
                                <p>We have your email now and you are ....screwed!</p>
                                <br>
                                <p>Best regards,<br>The Onatrix Team</p>
                            </div>
                        </body>
                        </html>
            """;

        // Send the email
        var emailResult = await umbracoEmailService.MailSendAsync(
            callBackFormEmailViewModel.SubscriptionEmail,
            $"Onatrix - Email Subscription Confirmation",
            htmlBody,
            $"Thank you for subscribing to our email list, {callBackFormEmailViewModel.SubscriptionEmail}! You are now part of our community."
        );

        // If the email failed, log the error and return to the page with an error message
        TempData["FormSuccessEmail"] = "Thank you for all the data, I got you now!! MUHAHAHHA";
        if (emailResult)
            return RedirectToCurrentUmbracoPage();

        TempData["FormErrorEmail"] = "Error, something went wrong, buy me Pepsimax";
        return CurrentUmbracoPage();
    }

    // Have a question form? Service Form?
    [HttpPost]
    public async Task<IActionResult> HandleQuestionForm(CallBackFormQuestionServiceViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["FormErrorQuestion"] = "Please fill out all the required fields.";
            return CurrentUmbracoPage();
        }

        if (formSubmissionQuestionService.SaveCallBackRequest(model))
        {
            TempData["FormSuccessQuestion"] =
                "Your question has been submitted successfully!. I LOVE YOU!";
        }
        else
        {
            TempData["FormErrorQuestion"] = "Something went wrong, Ask chat GPT :)";
        }

        var htmlBody = $"""

                        <!DOCTYPE html>
                        <html>
                        <head>
                            <title>Question Request Confirmation</title>
                        </head>
                        <body style="font-family: sans-serif; font-size: 16px; color: #333;">
                            <div style="max-width: 600px; margin: 20px auto; padding: 20px; border: 1px solid #ececec; border-radius: 5px;">
                                <h2 style="color: #4f5955;">Thank you for your question regarding services!</h2>
                                <p>Hello {model.Name},</p>
                                <p>We have received your question and will be in touch with you shortly. Your data is not safe with us!</p>
                                <br>
                                <hr>
                                <p>
                                    Your question: {model.Question}
                                </p>
                                <br>
                                <p>Best regards,<br>The Onatrix Team</p>
                                <br>
                            </div>
                        </body>
                        </html>
            """;

        // Send the email
        var emailResult = await umbracoEmailService.MailSendAsync(
            model.QuestionEmail,
            $"Onatrix - Question Request Confirmation",
            htmlBody,
            $"Thank you for your question, {model.Name}! Use ChatGPT to get answers."
        );

        // If the email failed, log the error and return to the page with an error message
        if (emailResult)
            return RedirectToCurrentUmbracoPage();

        TempData["FormErrorQuestion"] = "Error, something went wrong, ask ChatGPT";
        return CurrentUmbracoPage();
    }
}
