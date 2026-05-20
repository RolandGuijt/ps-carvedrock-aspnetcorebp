using CarvedRock_Shared.Data;

namespace CarvedRock_MVC.ApiServices;

public interface IProductApiService
{
    Task<Product?> Add(Product product);
    Task<IEnumerable<Product>> GetAll();
}
