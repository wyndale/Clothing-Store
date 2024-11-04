using ClothingStore.PaymentService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Explicitly bind to URLs here
builder.WebHost.UseUrls("https://localhost:7142", "http://localhost:5013");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDatabaseService(builder.Configuration);

builder.Services.AddPaymentService(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseCustomSwagger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
