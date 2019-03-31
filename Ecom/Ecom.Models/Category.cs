using System.Collections.Generic;


namespace Ecom.Models
{
    public class Category : BaseEntity
    {

        public List<Product> Products { get; set; }
    }
}
