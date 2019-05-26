using Ecom.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{

    public class ProductSearchViewModel
    {
        public int PageNo { get; set; }
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
    }

    public class NewProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageUrl { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

}