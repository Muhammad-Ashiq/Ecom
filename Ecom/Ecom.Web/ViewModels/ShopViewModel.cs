using Ecom.Models;
using Ecom.Web.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{

    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIds { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class ShopViewModel
    {
        public int MaximumPrice { get; set; }

        public List<Category> FeaturedCategories { get; set; }

        public List<Product> Products { get; set; }

        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }

        public Pager Pager { get; set; }
        public string SearchTerm { get; set; }
    }

    public class FilterProductsViewModel
    {
        public string SearchTearm { get; set; }
        public int? SortBy { get; set; }
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
    }
}