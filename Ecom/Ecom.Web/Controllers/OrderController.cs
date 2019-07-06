using System.Collections.Generic;
using Ecom.Service;
using Ecom.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class OrderController : Controller
    {
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
        // GET: Order
        public ActionResult Index(string userId, string status, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel();
            model.UserId = userId;
            model.Status = status;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var pageSize = ConfigService.Instance.PageSize();

            model.Orders = OrdersService.Instance.SearchOrders(userId, status, pageNo.Value, pageSize);

            var totalRecords = OrdersService.Instance.SearchOrderCount(userId, status);
            //model.Products = ProductService.Instance.GetProducts(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);


            return View(model);
        }
        public ActionResult Details(int Id)
        {

            OrderDetailsViewModel model = new OrderDetailsViewModel();

            model.Order = OrdersService.Instance.GetOrderdById(Id);

            if (model.Order != null)
            {
                model.OrderBy = UserManager.FindById(model.Order.UserId);
            }
            model.AvailableStatuses = new List<string>() { "Pending", "In Progress", "Delivered" };
            return View(model);
        }


        public JsonResult ChangeStatus(string status, int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = OrdersService.Instance.UpdateOrderStatus(Id, status) };

            return result;
        }
    }
}