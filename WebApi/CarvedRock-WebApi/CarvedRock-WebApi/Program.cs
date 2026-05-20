using CarvedRock_WebApi.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddSingleton<IProductRepository, 
    ProductRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

app.Run();
