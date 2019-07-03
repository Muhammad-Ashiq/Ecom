using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{

    public class ShopController : Controller
    {
        //ProductService productService = new ProductService();

        public ActionResult Index(string searchTerm, int? minimumPrice, int? categoryId, int? maximumPrice, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigService.Instance.ShopPageSize();

            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductService.Instance.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryId = categoryId;

            int totalCount = ProductService.Instance.SearchProductCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy);
            model.Products = ProductService.Instance.SearchProduct(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy, pageNo.Value, pageSize);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? pageNo, int? minimumPrice, int? categoryId, int? maximumPrice, int? sortBy)
        {
            var pageSize = ConfigService.Instance.ShopPageSize();

            FilterProductsViewModel model = new FilterProductsViewModel();
            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryId = categoryId;
            int totalCount = ProductService.Instance.SearchProductCount(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy);

            model.Products = ProductService.Instance.SearchProduct(searchTerm, minimumPrice, maximumPrice, categoryId, sortBy, pageNo.Value, pageSize);
            model.Pager = new Pager(totalCount, pageNo, pageSize);
            return PartialView(model);
        }

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