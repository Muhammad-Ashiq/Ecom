using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class ShopController : Controller
    {
        //ProductService productService = new ProductService();


        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            if (Request.Cookies["CartProducts"] != null)
            {
                //var ProductIds = CartProductCookie.Value;
                //var ids = ProductIds.Split('-');
                //List<int> pIds = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIds = Request.Cookies["CartProducts"].Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductService.Instance.GetProducts(model.CartProductIds);

            }

            return View(model);
        }
    }
}