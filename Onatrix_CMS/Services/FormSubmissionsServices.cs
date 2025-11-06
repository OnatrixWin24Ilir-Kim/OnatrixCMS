using Onatrix_CMS.ViewModels;
using Umbraco.Cms.Core.Services;

namespace Onatrix_CMS.Services;

public class FormSubmissionsServices(IContentService contentService)
{
    public bool SaveCallBackRequest(CallbackFormViewModel model)
    {
        try
        {
            // Getting Content Alias from BackOffice
            var container = contentService
                .GetRootContent()
                .FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var reqeustName = $"{DateTime.Now:yyy-MM-dd HH:mm:ss:fffffff} - {model.Name}";
            var formRequest = contentService.Create(reqeustName, container, "callBackRequest");
            // Getting Form Data
            formRequest.SetValue("callBackRequestName", model.Name);
            formRequest.SetValue("callBackReqeustEmail", model.Email);
            formRequest.SetValue("callbackRequestPhone", model.Phone);
            formRequest.SetValue("callBackRequestOptions", model.SelectedOption);

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
