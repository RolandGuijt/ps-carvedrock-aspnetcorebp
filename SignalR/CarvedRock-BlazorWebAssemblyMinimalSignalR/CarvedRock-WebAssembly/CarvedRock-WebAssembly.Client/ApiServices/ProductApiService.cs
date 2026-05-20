using CarvedRock_Shared.Data;
using System.Net.Http.Json;

namespace CarvedRock_WebAssembly.ApiServices;
public class ProductApiService(HttpClient httpClient) : IProductApiService
{
    public async Task<List<Product>> GetAll()
    {
        var response = await httpClient.GetAsync("product");
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();

        if (products == null)
            return Enumerable.Empty<Product>().ToList();
        return products.ToList();

    }

    public async Task<Product?> Add(Product product)
    {
        var jsonContent = JsonContent.Create(product);
        var response = await httpClient.PostAsync("product", jsonContent);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

}

