using ClothingStore.ProductService.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.ProductService.Extensions;

public static class DatabaseServiceExtension
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = "ProductServiceDatabase" ?? throw new Exception("connection string is null.");

        services.AddDbContext<ProductServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connectionStrings));
        });

        return services;
    }
}
