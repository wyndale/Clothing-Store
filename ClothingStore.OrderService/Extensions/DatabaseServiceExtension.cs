using ClothingStore.OrderService.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.OrderService.Extensions;

public static class DatabaseServiceExtension
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = "OrderServiceDatabase" ?? throw new Exception("connection string is null.");

        services.AddDbContext<OrderServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connectionStrings));
        });

        return services;
    }
}
