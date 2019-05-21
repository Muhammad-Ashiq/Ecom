using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ecom.Service
{
    public class CategoriesService
    {
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
        public Category GetCategory(int Id)
        {
            using (var context = new EContext())
            {
                return context.Categories.Find(Id);
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
        public void DeleteCategory(int Id)
        {
            using (var context = new EContext())
            {
                var category = context.Categories.Find(Id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
