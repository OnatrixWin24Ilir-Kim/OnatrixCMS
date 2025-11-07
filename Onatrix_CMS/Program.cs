using Onatrix_CMS.Services;

var builder = WebApplication.CreateBuilder(args);

builder
    .CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .AddAzureBlobMediaFileSystem()
    .AddAzureBlobImageSharpCache()
    .Build();

builder.Services.AddScoped<FormSubmissionsServices>();
builder.Services.AddScoped<FormSubmissionEmailService>();
builder.Services.AddScoped<FormSubmissionQuestionService>();

// Register Email Service
builder.Services.AddHttpClient<UmbracoEmailService>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["OnatrixEmailService:BaseUrl"]
            ?? throw new InvalidOperationException("Email Base url is missing.")
    );
});

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
