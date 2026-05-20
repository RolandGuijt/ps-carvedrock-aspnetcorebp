using CarvedRock_MVC.ApiServices;
using CarvedRock_Shared.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock_MVC.Controllers;

public class ProductController(IProductApiService productApiService) : 
    Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await productApiService.GetAll();
        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product newProduct)
    {
        if (!ModelState.IsValid)
            return View();

        await productApiService.Add(newProduct);

        return RedirectToAction("Index");
    }
}