using ClothingStore.IdentityService.Data;
using ClothingStore.IdentityService.Interfaces;
using ClothingStore.IdentityService.Services;
using Microsoft.AspNetCore.Identity;

namespace ClothingStore.IdentityService.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
