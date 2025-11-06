using Onatrix_CMS.ViewModels;
using Umbraco.Cms.Core.Services;

namespace Onatrix_CMS.Services;

public class FormSubmissionEmailService(IContentService contentService)
{
    public bool SaveCallBackRequest(CallBackFormEmailViewModel model)
    {
        try
        {
            // Getting Content Alias from BackOffice
            var container = contentService
                .GetRootContent()
                .FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var reqeustName = $"{DateTime.Now:yyy-MM-dd HH:mm:ss:fffffff} - {model.SubscriptionEmail}";
            var formRequest = contentService.Create(reqeustName, container, "emailCallBackRequest");
            // Getting Form Data
            formRequest.SetValue("emailCallBackEmail", model.SubscriptionEmail);

            // Save data to BackOffice
            var saveResult = contentService.Save(formRequest);

            // Returned the Save result
            return saveResult.Success;
        }
        catch (Exception)
        {
            return false;
        }
    }    
}