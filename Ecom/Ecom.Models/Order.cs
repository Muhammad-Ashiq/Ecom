using System;
using System.Collections.Generic;

namespace Ecom.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime OrderedAt { get; set; }

        public string Status { get; set; }

        public decimal TotalAmmount { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
