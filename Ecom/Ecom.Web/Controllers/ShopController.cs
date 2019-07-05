using Ecom.Models;
using Ecom.Service;
using Ecom.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{

    public class ShopController : Controller
    {
        //ProductService productService = new ProductService();

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



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
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductCookie = Request.Cookies["CartProducts"];

            if (CartProductCookie != null && !string.IsNullOrEmpty(CartProductCookie.Value))
            {
                //var ProductIds = CartProductCookie.Value;
                //var ids = ProductIds.Split('-');
                //List<int> pIds = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIds = Request.Cookies["CartProducts"].Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductService.Instance.GetProducts(model.CartProductIds);
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }

            return View(model);
        }

        //productIDs should beformatted like = "7-7-9-1"
        public JsonResult PlaceOrder(string productIds)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(productIds))
            {

                var productQuantities = productIds.Split('-').Select(x => int.Parse(x)).ToList();

                var boughtProducts = ProductService.Instance.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserId = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmmount =
                    boughtProducts.Sum(x => x.Price * productQuantities.Count(productId => productId == x.Id));

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem()
                {
                    ProductId = x.Id,
                    Quantity = productQuantities.Count(productId => productId == x.Id)
                }));


                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);


                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}