using System.Diagnostics.CodeAnalysis;
using DistributedLibrary.Data;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Services;
using DistributedLibrary.Shared.Configuration;
using DistributedLibrary.UI.Auth;
using DistributedLibrary.UI.Middlewares;
using Microsoft.AspNetCore.SignalR;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DistributedLibraryContext>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<TokenProvider>();
builder.Services.AddTransient<UserInfoProvider>();

//builder.Services.Configure<OpenIdConnectOptions>(
//    OpenIdConnectDefaults.AuthenticationScheme, options =>
//    {
//        options.ResponseType = OpenIdConnectResponseType.Code;
//        options.SaveTokens = true;

//        options.Scope.Add("offline_access");
//    });
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    

    googleOptions.CallbackPath = "/signin-google";
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(x => x.AddFilter(typeof(ExceptionHandlingSignalRFilter)));


builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.SectionName));
builder.Services.Configure<ApplicationInsightsConfiguration>(builder.Configuration.GetSection(ApplicationInsightsConfiguration.SectionName));
builder.Services.Configure<CommunicationServiceConfiguration>(builder.Configuration.GetSection(CommunicationServiceConfiguration.SectionName));
builder.Services.Configure<ApplicationConfiguration>(builder.Configuration.GetSection(ApplicationConfiguration.SectionName));

builder.Logging.AddApplicationInsights(
    configureTelemetryConfiguration: (config) =>
    {
        var appInsightsConfig = new ApplicationInsightsConfiguration();

        builder.Configuration
            .GetSection(ApplicationInsightsConfiguration.SectionName)
            .Bind(appInsightsConfig);

        // in case of build efbundle mode
        if (!string.IsNullOrEmpty(appInsightsConfig.ConnectionString))
        {
            config.ConnectionString = appInsightsConfig.ConnectionString;
        }
    },
    configureApplicationInsightsLoggerOptions: (options) => { });
builder.Services.AddApplicationInsightsTelemetry();


builder.Services.AddDistributedLibraryServices();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapRazorPages();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program
{
}
