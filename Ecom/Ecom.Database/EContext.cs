using System;
using Ecom.Models;
using System.Data.Entity;

namespace Ecom.Database
{
    public class EContext : DbContext
    {
        public EContext() : base("Ecom")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
