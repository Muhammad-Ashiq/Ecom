using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts, int? CategoryId)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();

            model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                model.Products = ProductService.Instance.GetLatestProducts(4);
            }
            else if (CategoryId.HasValue && CategoryId.Value > 0)
            {
                model.Products = ProductService.Instance.GetProductsByCategory(CategoryId.Value, 4);
            }
            else
            {
                model.Products = ProductService.Instance.GetProducts(1, 8);

            }
            return PartialView(model);
        }
    }
}