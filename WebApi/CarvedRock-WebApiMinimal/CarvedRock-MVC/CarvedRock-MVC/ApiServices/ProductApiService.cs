using CarvedRock_Shared.Data;

namespace CarvedRock_MVC.ApiServices;
public class ProductApiService(HttpClient httpClient) : IProductApiService
{
    public async Task<IEnumerable<Product>> GetAll()
    {
        var response = await httpClient.GetAsync("product");
        response.EnsureSuccessStatusCode();
        var products = await 
            response.Content.ReadFromJsonAsync<IEnumerable<Product>>();

        if (products == null)
            return [];
        return products;

    }
    public async Task<Product?> Add(Product product)
    {
        var jsonContent = JsonContent.Create(product);
        var response = await httpClient.PostAsync("product", jsonContent);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }
}
