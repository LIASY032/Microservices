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

    public class CheckoutController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();



        public CheckoutController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var userName = "swn";
            Cart = await _basketService.GetBasket(userName);

            return View(Cart);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userName = "swn";
            Cart = await _basketService.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return View();
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(Order);
            return View();
        }
    }
}

