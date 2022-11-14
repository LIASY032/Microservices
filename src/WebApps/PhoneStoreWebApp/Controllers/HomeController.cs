using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWebApp.Models;
using PhoneStoreWebApp.Services;

namespace PhoneStoreWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;
    public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();
    public HomeController(ICatalogService catalogService, IBasketService basketService)
    {
        _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public async Task<IActionResult> Index()
    {
        ProductList = await _catalogService.GetCatalog();
        return View(ProductList);
    }


    public IActionResult Contact() {

        return View();
    }

    public async Task<IActionResult> AddToCart(string productId) {
        var product = await _catalogService.GetCatalog(productId);

        var userName = "swn";
        var basket = await _basketService.GetBasket(userName);

        basket.Items.Add(new BasketItemModel
        {
            ProductId = productId,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = 1,
            Color = "Black"
        });

        var basketUpdated = await _basketService.UpdateBasket(basket);
        return RedirectToAction(nameof(CartController.Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(string name) {

        if (ProductList.Count() < 1) { 
	
            ProductList = await _catalogService.GetCatalog();
        }

        var newList = ProductList.Where(item => item.Name.Contains(name));


        return View(newList);
    
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

