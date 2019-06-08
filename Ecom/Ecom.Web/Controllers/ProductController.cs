using Ecom.Models;
using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class ProductController : Controller
    {
        //CategoriesService categoriesService = new CategoriesService();
        //ProductService productService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();

            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Products = ProductService.Instance.GetProducts(model.PageNo);

            if (string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where
                    (p => p.Name != null && p.Name.
                ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView(model);
        }



        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            //newProduct.CategoryId = model.CategoryId;
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryId);
            newProduct.ImagrUrl = model.ImageUrl;
            ProductService.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductService.Instance.GetProduct(id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryId = product.Category != null ? product.Category.Id : 0;
            model.ImageUrl = product.ImagrUrl;
            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductService.Instance.GetProduct(model.Id);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryId);
            existingProduct.ImagrUrl = model.ImageUrl;
            ProductService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            ProductService.Instance.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Details(int Id)
        {
            ProductViewModels model = new ProductViewModels();

            model.Product = ProductService.Instance.GetProduct(Id);

            return View(model);
        }

    }
}