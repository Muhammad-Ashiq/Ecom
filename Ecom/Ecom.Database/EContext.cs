using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Ecom.Models;

namespace Ecom.Database
{
    public class EContext:DbContext
    {
        public EContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
