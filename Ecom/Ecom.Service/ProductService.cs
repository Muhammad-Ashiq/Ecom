using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ecom.Service
{
    public class ProductService
    {
        #region Singleton
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();

                return instance;
            }
        }
        private static ProductService instance { get; set; }

        private ProductService()
        {
        }

        #endregion

        public Product GetProduct(int id)
        {
            using (var context = new EContext())
            {
                return context.Products.Where(c => c.Id == id).
                    Include(x => x.Category).
                    FirstOrDefault();
            }
        }

        public List<Product> GetProducts(List<int> ids)
        {
            using (var context = new EContext())
            {
                return context.Products.Where(x => ids.Contains(x.Id)).ToList();
            }
        }

        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;
            using (var context = new EContext())
            {
                return context.Products.OrderBy(x => x.Id).Skip((pageNo - 1) * pageSize).
                    Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();
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

        public void DeleteProduct(int id)
        {
            using (var context = new EContext())
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }



        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {

            using (var context = new EContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Take(numberOfProducts).Include(x => x.Category).ToList();

            }

        }

        public List<Product> GetProducts(int pageNo, int pageSize)
        {

            using (var context = new EContext())
            {
                return context.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).
                    Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();
            }

        }
        public List<Product> GetProductsByCategory(int categoryId, int pageSize)
        {

            using (var context = new EContext())
            {
                return context.Products.Where(x => x.Category.Id == categoryId).OrderByDescending(x => x.Id).
                    Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();
            }

        }


    }



}