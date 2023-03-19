using DistributedLibrary.Services;
using DistributedLibrary.Shared.Configuration;
using DistributedLibrary.UI.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<WeatherForecastService>();


builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.SectionName));
builder.Services.Configure<ApplicationInsightsConfiguration>(builder.Configuration.GetSection(ApplicationInsightsConfiguration.SectionName));

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

app.Run();
