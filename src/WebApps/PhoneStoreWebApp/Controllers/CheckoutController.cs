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
        private CheckoutVM checkoutVM;
    
        public CheckoutController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            checkoutVM = new CheckoutVM();

        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var userName = "swn";
            checkoutVM.Cart = await _basketService.GetBasket(userName);

            return View(checkoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(BasketCheckoutModel checkoutModel)
        {
            var userName = "swn";
            checkoutVM.Cart = await _basketService.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return View();
            }
            checkoutVM.Order = checkoutModel;

            checkoutVM.Order.UserName = userName;
            checkoutVM.Order.TotalPrice = checkoutVM.Cart.TotalPrice;

            await _basketService.CheckoutBasket(checkoutVM.Order);
            return RedirectToAction(nameof (Index));
        }
    }
}

