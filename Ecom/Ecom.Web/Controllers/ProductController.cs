using Ecom.Models;
using Ecom.Service;
using System.Linq;
using System.Web.Mvc;

namespace Ecom.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            var product = productService.GetProducts();
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.Name.Contains(search)).ToList();
            }
            return PartialView(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            productService.SaveProduct(product);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pro = productService.GetProduct(id);
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProduct(product);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            productService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}