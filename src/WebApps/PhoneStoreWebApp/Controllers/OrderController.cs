using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneStoreWebApp.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneStoreWebApp.Controllers
{
    public class OrderController : Controller
    {
        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            Orders = await _orderService.GetOrdersByUserName("swn");

            return View(Orders);
        }
    }
}

