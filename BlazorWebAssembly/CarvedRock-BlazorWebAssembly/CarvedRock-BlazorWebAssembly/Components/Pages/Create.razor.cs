using CarvedRock_BlazorServer.Data;
using Microsoft.AspNetCore.Components;

namespace CarvedRock_BlazorServer.Components.Pages
{
    public partial class Create
    {        
        [SupplyParameterFromForm]
        private Product NewProduct { get; set; } = default!;

        private string? _nameValidationError;

        protected override void OnInitialized()
        {
            NewProduct ??= new();
        }

        [Inject]
        private IProductRepository ProductRepository { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private async Task OnNameInput(ChangeEventArgs e)
        {
            var name = e.Value?.ToString();
            if (string.IsNullOrWhiteSpace(name))
            {
                _nameValidationError = null;
                return;
            }

            var products = await ProductRepository.GetAll();
            if (products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                _nameValidationError = "Name already taken";
            }
            else
            {
                _nameValidationError = null;
            }
        }

        private async Task CreateProduct()
        {
            if (!string.IsNullOrEmpty(_nameValidationError))
            {
                return;
            }

            await ProductRepository.Add(NewProduct);
            NavigationManager.NavigateTo("/");
        }
    }
}
