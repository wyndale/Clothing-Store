namespace ClothingStore.OrderService.Extensions;

public static class AuthorizationServiceExtension
{
    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole("Admin"));
        });

        return services;
    }
}
