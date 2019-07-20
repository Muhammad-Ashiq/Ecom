using Ecom.Models;
using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    [Authorize(Roles = "CanManage")]
    public class CategoryController : Controller
    {
        //[Authorize(Roles = "Admin")]
        //now we use singalton method to access this service
        //CategoriesService categoryService = new CategoriesService();
        [HttpGet]
        public ActionResult Index()

        {
            return View();
        }


        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);
            model.Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {

                model.Pager = new Pager(totalRecords, pageNo, 3);

                return PartialView("_CategoryTable", model);

            }
            else
            {
                return HttpNotFound();

            }
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
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageUrl = model.ImageUrl;
                newCategory.IsFeatured = model.IsFeatured;
                CategoriesService.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            return new HttpStatusCodeResult(500);


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