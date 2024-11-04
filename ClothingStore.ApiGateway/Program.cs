using JwtConfiguration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration)
    .AddPolly();

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddFilter("Microsoft", LogLevel.Debug);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();


app.UseHttpsRedirection();

app.UseForwardedHeaders();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();
