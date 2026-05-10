using CarvedRock_RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarvedRock_RazorPages.Pages
{
    public class CreateModel(IProductRepository productRepository) : 
        PageModel
    {
        private readonly IProductRepository productRepository = 
            productRepository;

        [BindProperty]
        public Product NewProduct { get; set; } = new();
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await productRepository.Add(NewProduct);

            return RedirectToPage("Index");
        }
    }
}
