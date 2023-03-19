using DistributedLibrary.Data;
using DistributedLibrary.Services.Mapping;
using DistributedLibrary.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DistributedLibrary.Services;

public static class Register
{
    public static void AddDistributedLibraryServices(this IServiceCollection services)
    {
        services.AddDistributedLibraryData();
        services.AddTransient<LibraryService>();

        services.AddAutoMapper(typeof(AutomapperProfile));
    }
}