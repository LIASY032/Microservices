using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWebApp.Models;
using PhoneStoreWebApp.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneStoreWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private ProductDetailVM productDetail;
        private ProductVM product;

        public ProductController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            productDetail = new ProductDetailVM();
            product = new ProductVM();
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string categoryName)
        {
            var productList = await _catalogService.GetCatalog();
            product.CategoryList = productList.Select(p => p.Category).Distinct();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                product.ProductList = productList.Where(p => p.Category == categoryName);
                product.SelectedCategory = categoryName;
            }
            else
            {
                product.ProductList = productList;
            }

            return View(product);
        }
      

  


        public async Task<IActionResult> Detail(string productId) {


            if (productId == null)
            {
                return NotFound();
            }

            productDetail.Product = await _catalogService.GetCatalog(productId);
            if (productDetail.Product == null)
            {
                return NotFound();
            }


            return View(productDetail);


        }


 //       [HttpPost]
 //       public async Task<IActionResult> AddToCart(string productId) {
 //           var product = await _catalogService.GetCatalog(productId);

 //           var userName = "swn";
 //           var basket = await _basketService.GetBasket(userName);

 //           basket.Items.Add(new BasketItemModel
 //           {
 //               ProductId = productId,
 //               ProductName = product.Name,
 //               Price = product.Price,
 //               Quantity = productDetail.Quantity,
 //               Color = productDetail.Color
 //           });

 //           var basketUpdated = await _basketService.UpdateBasket(basket);



 //           return View();
	//}

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int Quantity, string Color)
        {
            var product = await _catalogService.GetCatalog(productId);

            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = Quantity,
                Color = Color
            });

            var basketUpdated = await _basketService.UpdateBasket(basket);



            return RedirectToAction(nameof(HomeController.Index)); ;
        }
    }
}

