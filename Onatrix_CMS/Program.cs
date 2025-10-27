using Onatrix_CMS;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.ConfigureKeyVault();

builder.CreateUmbracoBuilder().AddBackOffice().AddWebsite().AddDeliveryApi().AddComposers().Build();

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
