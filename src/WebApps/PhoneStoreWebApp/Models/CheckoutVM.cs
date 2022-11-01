using System;
using Microsoft.AspNetCore.Mvc;

namespace PhoneStoreWebApp.Models
{
    public class CheckoutVM
    {
        public BasketCheckoutModel Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();



    }
}

