using DistributedLibrary.Data.Interfaces;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DistributedLibrary.Data;

public static class Register
{
    public static void AddDistributedLibraryData(this IServiceCollection services)
    {
        services.AddTransient<LibraryRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<DistributedLibraryContext>((sp, x) =>
        {
            var connectionString = sp.GetRequiredService<IOptions<DatabaseConfiguration>>();
            
            x.UseSqlServer(connectionString.Value.ConnectionString);
        });
    }
}