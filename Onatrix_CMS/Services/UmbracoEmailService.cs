namespace Onatrix_CMS.Services;

/// <summary>
/// Service for sending emails using Umbraco's built-in email API.
/// </summary>
/// <param name="httpClient"></param>
public class UmbracoEmailService(HttpClient httpClient)
{
    public async Task<bool> MailSendAsync(
        string to,
        string subject,
        string htmlBody,
        string plainTextBody
    )
    {
        // Creating Email Request Object
        var emailRequest = new
        {
            to,
            subject,
            htmlBody,
            plainTextBody,
        };

        // Try Catch block for asynchronous operation
        try
        {
            // Using built in :NET httpClient to call Email external API email service
            var apiResponse = await httpClient.PostAsJsonAsync("/Mail/send", emailRequest);

            // Check if the API response is successful
            if (apiResponse.IsSuccessStatusCode)
                return true;

            // Log the error and send the payload
            var payload = await apiResponse.Content.ReadAsStringAsync();
            return false;
        }
        // Log the exception
        catch (Exception)
        {
            return false;
        }
    }
}
