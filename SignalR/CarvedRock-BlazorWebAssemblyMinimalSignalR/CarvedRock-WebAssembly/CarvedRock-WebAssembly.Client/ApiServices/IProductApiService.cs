using CarvedRock_Shared.Data;

namespace CarvedRock_WebAssembly.ApiServices;

public interface IProductApiService
{
    Task<Product?> Add(Product product);
    Task<List<Product>> GetAll();
}
