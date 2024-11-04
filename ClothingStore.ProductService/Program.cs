using ClothingStore.ProductService.Extensions;
using JwtConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Explicitly bind to URLs here
builder.WebHost.UseUrls("https://localhost:7235", "http://localhost:5045");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCustomAuthorization(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseCustomSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
