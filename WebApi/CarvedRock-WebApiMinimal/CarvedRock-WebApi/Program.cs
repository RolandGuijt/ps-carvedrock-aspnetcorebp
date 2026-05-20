using CarvedRock_WebApi.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/product", async (IProductRepository productRepository) =>
{
    var products = await productRepository.GetAll();
    if (!products.Any())
        return Results.NoContent();

    return Results.Ok(products);
});

app.MapGet("/product/{id:int}", async (int id, 
    IProductRepository productRepository) =>
{
    var product = await productRepository.GetOne(id);

    if (product == null)
        return Results.NotFound();

    return Results.Ok(product);
})
    .WithName("GetOne");

app.MapPost("/product", async (Product product, 
    IProductRepository productRepository) =>
{
    await productRepository.Add(product);
    return Results.CreatedAtRoute("GetOne", new { id = product.Id }, 
        product);
});

app.MapOpenApi();
app.MapScalarApiReference();

app.Run();
