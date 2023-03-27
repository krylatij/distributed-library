using Azure.Communication.Email;
using DistributedLibrary.Data;
using DistributedLibrary.Services.Mapping;
using DistributedLibrary.Services.Services;
using DistributedLibrary.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Services;

[ExcludeFromCodeCoverage]
public static class Register
{
    public static void AddDistributedLibraryServices(this IServiceCollection services)
    {
        services.AddDistributedLibraryData();
        services.AddTransient<LibraryService>();
        services.AddTransient<GridService>();
        services.AddTransient<NotificationService>();

        services.AddAutoMapper(typeof(AutomapperProfile));

        services.AddSingleton(x =>
        {
            var config = x.GetRequiredService<IOptions<CommunicationServiceConfiguration>>();

            return new EmailClient(config.Value.ConnectionString);
        });
    }
}