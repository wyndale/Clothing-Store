using ClothingStore.IdentityService.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.IdentityService.Extensions;

public static class DatabaseServiceExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = "ClothingStore.data";

        if (connectionStrings == null) throw new Exception("Database connection problem.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connectionStrings));
        });

        return services;
    }
}
