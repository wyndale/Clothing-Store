using ClothingStore.IdentityService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Explicitly bind to URLs here
builder.WebHost.UseUrls("https://localhost:7135", "http://localhost:5158");

builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddFilter("Microsoft", LogLevel.Debug);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddIdentityServices();

builder.Services.AddSwaggerDocumentation();


var app = builder.Build();

app.UseStaticFiles();

await app.SeedDatabaseAsync();

app.UseCustomSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
