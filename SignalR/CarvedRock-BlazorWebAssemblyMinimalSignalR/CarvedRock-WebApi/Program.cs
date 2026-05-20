using CarvedRock_WebApi.Data;
using CarvedRock_WebApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi();
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
 
var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(b => { 
    b.WithOrigins("https://localhost:7094"); 
    b.AllowAnyHeader(); 
    b.AllowAnyMethod(); 
});

app.MapGet("/product", async (IProductRepository productRepository) =>
{
    var products = await productRepository.GetAll();
    if (!products.Any())
        return Results.NoContent();

    return Results.Ok(products);
});

app.MapGet("/product/{id:int}", async (int id, IProductRepository productRepository) =>
{
    var product = await productRepository.GetOne(id);

    if (product == null)
        return Results.NotFound();

    return Results.Ok(product);
})
    .WithName("GetOne");

app.MapPost("/product", async (Product product, 
    IProductRepository productRepository, 
    IHubContext<ProductHub> hubContext) =>
{
    await productRepository.Add(product);
    await hubContext.Clients.All.SendAsync("NewProduct", product);
    return Results.CreatedAtRoute("GetOne", new { id = product.Id }, 
        product);
});

app.MapOpenApi();
app.MapScalarApiReference();
app.MapHub<ProductHub>("/producthub");

app.Run();
