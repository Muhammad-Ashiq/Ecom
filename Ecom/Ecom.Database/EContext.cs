using Ecom.Models;
using System;
using System.Data.Entity;

namespace Ecom.Database
{
    public class EContext : DbContext, IDisposable
    {
        public EContext() : base("Ecom")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
