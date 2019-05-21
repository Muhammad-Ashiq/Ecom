using Ecom.Models;
using Ecom.Service;
using Ecom.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class ProductController : Controller
    {
        CategoriesService categoriesService = new CategoriesService();
        ProductService productService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.Products = productService.GetProducts();

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

            model.AvailableCategories = categoriesService.GetCategories();
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
            newProduct.Category = categoriesService.GetCategory(model.CategoryId);

            productService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = productService.GetProduct(Id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryId = product.Category != null ? product.Category.Id : 0;

            model.AvailableCategories = categoriesService.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = productService.GetProduct(model.Id);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = categoriesService.GetCategory(model.CategoryId);

            productService.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            productService.DeleteProduct(id);

            return RedirectToAction("ProductTable");
        }
    }
}