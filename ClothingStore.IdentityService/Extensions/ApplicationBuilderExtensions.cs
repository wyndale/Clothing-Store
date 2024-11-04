using ClothingStore.IdentityService.Services;

namespace ClothingStore.IdentityService.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            await RoleSeeder.SeedRolesAsync(serviceProvider);
        }
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service");

                options.InjectJavascript("/swagger/custom.js");
            });
        }
        return app;
    }
}
