using Ecom.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> FeaturedProducts { get; set; }
                
    }
}