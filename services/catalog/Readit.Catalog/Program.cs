using Readit.Catalog.Components;
using Microsoft.Extensions.Options;
using Readit.Catalog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOptions<ReadingTipsOptions>()
    .BindConfiguration(ReadingTipsOptions.SectionName)
    .Validate(options => !options.Enabled ||
                         (Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out var uri) &&
                          (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)),
        "ReadingTips:BaseUrl deve ser uma URL HTTP ou HTTPS quando ReadingTips:Enabled estiver habilitado.")
    .ValidateOnStart();

builder.Services.AddHttpClient<ReadingTipsClient>((services, client) =>
{
    var options = services.GetRequiredService<IOptions<ReadingTipsOptions>>().Value;
    if (options.Enabled)
    {
        client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
    }

    client.Timeout = TimeSpan.FromSeconds(3);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();