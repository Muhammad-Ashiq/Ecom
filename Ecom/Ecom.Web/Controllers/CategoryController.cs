using Ecom.Models;
using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class CategoryController : Controller
    {
        
        CategoriesService categoryService = new CategoriesService();
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.Categories = categoryService.GetCategories();

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
            categoryService.SaveCategory(newCategory);

            return RedirectToAction("CategoryTable");

        }


        [HttpPost]
        public ActionResult Save(Category category)
        {
            categoryService.SaveCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = categoryService.GetCategory(id);

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
            var existingCategory = categoryService.GetCategory(model.Id);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageUrl = model.ImageUrl;
            existingCategory.IsFeatured = model.IsFeatured;


            categoryService.UpdateCategory(existingCategory);
            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            categoryService.DeleteCategory(Id);

            return RedirectToAction("CategoryTable");
        }

    }
}