using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWebApp.Models;
using PhoneStoreWebApp.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneStoreWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly IBasketService _basketService;
        public BasketModel Cart { get; set; } = new BasketModel();
        public CartController(IBasketService basketService) {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            // TODO: add the real user
            var userName = "swn";
            Cart = await _basketService.GetBasket(userName);
            return View(Cart);
        }

        // Post
        [HttpPost]
        public async Task<IActionResult> RemoveToCart(string productId)
        {
            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);

            var item = basket.Items.Single(x => x.ProductId == productId);
            basket.Items.Remove(item);

            var basketUpdated = await _basketService.UpdateBasket(basket);

            return RedirectToAction(nameof(Index));
        }
    }
}

