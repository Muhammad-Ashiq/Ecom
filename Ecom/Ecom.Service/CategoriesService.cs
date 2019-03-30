using Ecom.Database;
using Ecom.Models;
using System.Collections.Generic;
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
                return context.Categories.ToList();
            }
        }
    }
}
