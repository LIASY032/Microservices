using System;
using Microsoft.AspNetCore.Mvc;

namespace PhoneStoreWebApp.Models
{
    public class ProductDetailVM
    {
        [BindProperty]
        public string Color { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public CatalogModel Product { get; set; }
    }
}

