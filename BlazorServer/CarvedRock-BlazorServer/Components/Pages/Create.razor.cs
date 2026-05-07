using CarvedRock_BlazorServer.Data;
using Microsoft.AspNetCore.Components;

namespace CarvedRock_BlazorServer.Components.Pages;

public partial class Create
{   
    private string? _nameValidationError;
    private IEnumerable<Product> Products { get; set; } = [];
        
    [SupplyParameterFromForm]
    public required Product NewProduct { get; set; }
        
    [Inject] 
    public required IProductRepository ProductRepository { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }
        
    protected override async Task OnInitializedAsync()
    {
        NewProduct = new Product();
        Products = await ProductRepository.GetAll();
    }

    private void OnNameInput(ChangeEventArgs e)
    {
        var name = e.Value?.ToString();
        if (string.IsNullOrWhiteSpace(name))
        {
            _nameValidationError = null;
            return;
        }
            
        if (Products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
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