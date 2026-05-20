using CarvedRock_WebAssembly.ApiServices;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddHttpClient<IProductApiService, ProductApiService>(o =>
    o.BaseAddress = new Uri("https://localhost:7273"));

await builder.Build().RunAsync();
