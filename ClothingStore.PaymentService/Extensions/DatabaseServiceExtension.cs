using ClothingStore.PaymentService.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.PaymentService.Extensions;

public static class DatabaseServiceExtension
{
    public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = "PaymentServiceDatabase" ?? throw new Exception("connection string is null.");

        services.AddDbContext<PaymentServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(connectionStrings));
        });

        return services;
    }
}
