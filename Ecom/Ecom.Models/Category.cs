using System.Collections.Generic;
using Microsoft.Build.Framework;


namespace Ecom.Models
{
    public class Category : BaseEntity
    {
        
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; }
        public bool IsFeatured { get; set; }
    }
}
