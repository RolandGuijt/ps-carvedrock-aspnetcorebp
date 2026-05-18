using CarvedRock_BlazorWebAssembly.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton<IProductRepository, 
    ProductRepository>();

await builder.Build().RunAsync();
