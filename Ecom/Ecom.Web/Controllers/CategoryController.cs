using Ecom.Models;
using Ecom.Service;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesService service = new CategoriesService();
        [HttpGet]
        public ActionResult Index()
        {
            var categories = service.GetCategories();
            return View(categories);
        }
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            service.SaveCategory(category);
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            return View();
        }

        
    }
}