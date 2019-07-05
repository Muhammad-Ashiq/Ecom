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

        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {

            using (var context = new EContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(product => product.Name != null &&
                                                             product.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }

            }
        }


        public int GetProductsCount(string search)
        {

            using (var context = new EContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products
                        .Count(product => product.Name != null &&
                                                             product.Name.ToLower().Contains(search.ToLower()));
                }
                else
                {
                    return context.Products.Count();
                }

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


        public int GetMaximumPrice()
        {
            using (var context = new EContext())
            {
                return (int)context.Products.Max(x => x.Price);
            }
        }

        public List<Product> SearchProduct(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy, int pageNo, int pageSize)
        {
            using (var context = new EContext())
            {
                var product = context.Products.ToList();

                if (categoryId.HasValue)
                {
                    product = product.Where(x => x.Category.Id == categoryId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    product = product.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    product = product.Where(x => x.Price >= maximumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            product = product.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            product = product.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            product = product.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return product.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }


        }

        public int SearchProductCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryId, int? sortBy)
        {
            using (var context = new EContext())
            {
                var product = context.Products.ToList();

                if (categoryId.HasValue)
                {
                    product = product.Where(x => x.Category.Id == categoryId.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    product = product.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    product = product.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    product = product.Where(x => x.Price >= maximumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            product = product.OrderByDescending(x => x.Id).ToList();
                            break;
                        case 3:
                            product = product.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            product = product.OrderByDescending(x => x.Price).ToList();
                            break;
                    }
                }
                return product.Count;
            }


        }

    }



}