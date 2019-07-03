using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class Product : BaseEntity
    {
        [Range(1, 100000)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ImagrUrl { get; set; }

    }
}
