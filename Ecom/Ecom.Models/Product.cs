namespace Ecom.Models
{
    public class Product : BaseEntity
    {
        public decimal Price { get; set; }
        public int CategoryId { get; set; }


        public virtual Category Category { get; set; }

    }
}
