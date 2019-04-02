using Ecom.Database;
using Ecom.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class CategoryController : Controller
    {
        private EContext _context;

        public CategoryController()
        {
            _context = new EContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //CategoriesService service = new CategoriesService();

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        // GET: Category

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (category.Id == 0)
                _context.Categories.Add(category);
            else
            {
                var CategoryInDb = _context.Categories.Single(c => c.Id == category.Id);
                CategoryInDb.Name = category.Name;
                CategoryInDb.Description = category.Description;

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }

        public ActionResult Edit(int id)
        {
            var cate = _context.Categories.SingleOrDefault(m => m.Id == id);

            if (cate == null)
                return HttpNotFound();
            return View(cate);
        }

        public ActionResult Delete(int? id)
        {
            Category cate = _context.Categories.Find(id);
            _context.Categories.Remove(cate);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        




    }
}