using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ecom.Service
{
    public class ProductService
    {
        public Product GetProduct(int Id)
        {
            using (var context = new EContext())
            {
                return context.Products.Where(c => c.Id == Id).
                    Include(x => x.Category).
                    FirstOrDefault();
            }
        }

        public List<Product> GetProducts()
        {

            using (var context = new EContext())
            {
                return context.Products.Include(x => x.Category).ToList();
            }

        }

        public void SaveProduct(Product product)
        {
            using (var context = new EContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();
            }

        }

        public void UpdateProduct(Product product)
        {
            using (var context = new EContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void DeleteProduct(int Id)
        {
            using (var context = new EContext())
            {
                var product = context.Products.Find(Id);
                context.Products.Remove(product);
                context.SaveChanges();
            }



        }
    }
}