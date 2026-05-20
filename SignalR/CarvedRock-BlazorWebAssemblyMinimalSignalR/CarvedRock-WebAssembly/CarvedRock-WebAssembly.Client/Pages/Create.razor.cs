using CarvedRock_Shared.Data;
using CarvedRock_WebAssembly.ApiServices;
using Microsoft.AspNetCore.Components;

namespace CarvedRock_BlazorServer.Components.Pages;

public partial class Create
{   
    private string? _nameValidationError;
    private IEnumerable<Product> Products { get; set; } = [];
        
    [SupplyParameterFromForm]
    public required Product NewProduct { get; set; }
        
    [Inject] 
    public required IProductApiService ProductApiService { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }
        
    protected async Task OnInitializedAsync()
    {
        NewProduct = new Product();
        Products = await ProductApiService.GetAll();
    }

    private void OnNameInput(ChangeEventArgs e)
    {
        var name = e.Value?.ToString();
        
        if (Products.Any(p => p.Name.Equals(name, 
            StringComparison.OrdinalIgnoreCase)))
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

        await ProductApiService.Add(NewProduct);
        NavigationManager.NavigateTo("/");
    }
}