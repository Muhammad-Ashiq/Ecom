using Ecom.Models;
using System.Collections.Generic;

namespace Ecom.Web.ViewModels
{

    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIds { get; set; }
    }
}