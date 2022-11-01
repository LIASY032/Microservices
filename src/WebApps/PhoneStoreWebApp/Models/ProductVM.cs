using System;
using Microsoft.AspNetCore.Mvc;

namespace PhoneStoreWebApp.Models
{
    public class ProductVM
    {
        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }
    }
}

