using Ecom.Models;
using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class CategoryController : Controller
    {
        //now we use singalton method to access this service
        //CategoriesService categoryService = new CategoriesService();
        [HttpGet]
        public ActionResult Index()

        {

            return View();
        }


        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.Categories = CategoriesService.Instance.GetCategories();

            if (!string.IsNullOrEmpty(search))
            {
                model.SearchTerm = search;

                model.Categories = model.Categories.Where(p =>
                p.Name != null && p.Name.ToLower().
                Contains(search.ToLower())).ToList();
            }

            return PartialView("_CategoryTable", model);
        }

        // GET: Category
        #region Creation
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);

        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageUrl = model.ImageUrl;
            newCategory.IsFeatured = model.IsFeatured;
            CategoriesService.Instance.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");

        }
        #endregion

        #region Updation

        [HttpPost]
        public ActionResult Save(Category category)
        {
            CategoriesService.Instance.SaveCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoriesService.Instance.GetCategory(id);

            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageUrl = category.ImageUrl;
            model.IsFeatured = category.IsFeatured;

            return PartialView(model);


        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesService.Instance.GetCategory(model.Id);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageUrl = model.ImageUrl;
            existingCategory.IsFeatured = model.IsFeatured;


            CategoriesService.Instance.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }
        #endregion
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CategoriesService.Instance.DeleteCategory(id);

            return RedirectToAction("CategoryTable");
        }

    }
}