using Azure.Identity;

namespace Onatrix_CMS;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureKeyVault(this WebApplicationBuilder builder)
    {
        var keyVaultEndpoint = builder.Configuration["AzureKeyVaultEndpoint"];
        if (
            !string.IsNullOrWhiteSpace(keyVaultEndpoint)
            && Uri.TryCreate(keyVaultEndpoint, UriKind.Absolute, out var validUri)
        )
        {
            builder.Configuration.AddAzureKeyVault(validUri, new DefaultAzureCredential());
        }

        return builder;
    }
}
