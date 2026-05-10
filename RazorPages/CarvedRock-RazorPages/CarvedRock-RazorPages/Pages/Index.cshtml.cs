using CarvedRock_RazorPages.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarvedRock_RazorPages.Pages;

public class IndexModel(IProductRepository productRepository) : PageModel
{
    private readonly IProductRepository productRepository = productRepository;

    public IEnumerable<Product> Products { get; set; } = [];

    public async Task OnGet()
    {
        Products = await productRepository.GetAll();
    }
} 