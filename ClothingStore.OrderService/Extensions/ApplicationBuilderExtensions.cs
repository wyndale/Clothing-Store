namespace ClothingStore.OrderService.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service");
            });
        }

        return app;
    }
}
