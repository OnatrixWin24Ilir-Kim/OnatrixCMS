using Onatrix_CMS.ViewModels;
using Umbraco.Cms.Core.Services;

namespace Onatrix_CMS.Services;

public class FormSubmissionQuestionService(IContentService contentService)
{
    public bool SaveCallBackRequest(CallBackFormQuestionServiceViewModel model)
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
            var formRequest = contentService.Create(reqeustName, container, "questionCallBackRequest");
            // Getting Form Data
            formRequest.SetValue("questionCallBackName", model.Name);
            formRequest.SetValue("questionCallEmail", model.QuestionEmail);
            formRequest.SetValue("questionCallBackText", model.Question);

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