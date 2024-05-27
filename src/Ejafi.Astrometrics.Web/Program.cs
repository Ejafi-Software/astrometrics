using Ejafi.Astrometrics.Web;
using Ejafi.Astrometrics.Web.Components;
using Ejafi.Astrometrics.Web.Services.PointsOfInterest;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOutputCache();
builder.Services.AddHttpClient<AstrometricsApiClient>(client =>
{
    client.BaseAddress = new Uri("https+http://apiservice");
});
builder.Services.AddScoped<IPointsOfInterestService, PointOfInterestService>();

builder.WebHost.UseStaticWebAssets();
builder.Services.AddRadzenComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
