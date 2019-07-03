using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ecom.Service
{
    public class CategoriesService
    {

        #region Singleton

        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();

                return instance;
            }
        }

        private static CategoriesService instance { get; set; }

        public CategoriesService()
        {
        }

        #endregion

        public void SaveCategory(Category category)
        {
            
            using (var context = new EContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        //public List<Category> GetCategories()
        //{
        //    using (var context = new EContext())
        //    {
        //        return context.Categories.Include(x => x.Products).ToList();
        //    }
        //}
        public int GetCategoriesCount(string search)
        {
            using (var context = new EContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Count(category => category.Name != null &&
                                                                category.Name.ToLower().Contains(search.ToLower()));
                    //return context.Categories.Where(category => category.Name != null &&
                    //category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return context.Categories.Count();
                }
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var context = new EContext())
            {

                return context.Categories
                    .ToList();
            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new EContext())
            {
                return context.Categories.Where(x => x.IsFeatured && x.ImageUrl != null).ToList();
            }
        }

        public Category GetCategory(int id)
        {
            using (var context = new EContext())
            {
                return context.Categories.Find(id);
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new EContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void DeleteCategory(int id)
        {
            using (var context = new EContext())
            {
                var category = context.Categories.Where(x => x.Id == id).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(category.Products);//first delete product of this category
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 3;

            using (var context = new EContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                                                                category.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }

            }
        }
    }
}
