using Ecom.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{
    public class ProductsWidgetViewModel
    {
        public List<Product> Products { get; set; }

        public bool IsLatestProducts { get; set; }
    }
}