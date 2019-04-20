using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ecom.Service
{
    public class ProductService
    {
        public Product GetProduct(int Id)
        {
            using (var context = new EContext())
            {
                return context.Products.Find(Id);
            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new EContext())
            {
                return context.Products.ToList();
            }

        }

        public void SaveProduct(Product product)
        {
            using (var context = new EContext())
            {
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