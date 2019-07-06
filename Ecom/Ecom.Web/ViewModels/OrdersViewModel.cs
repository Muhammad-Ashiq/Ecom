using Ecom.Models;
using Ecom.Web.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserId { get; set; }
        public Pager Pager { get; set; }
        public string Status { get; set; }
    }
    public class OrderDetailsViewModel
    {
        public List<string> AvailableStatuses { get; set; }
        public Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
    }

}