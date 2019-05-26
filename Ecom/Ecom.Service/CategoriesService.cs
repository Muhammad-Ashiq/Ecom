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
        public List<Category> GetCategories()
        {
            using (var context = new EContext())
            {
                return context.Categories.Include(x => x.Products).ToList();
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
                var category = context.Categories.Find(id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
