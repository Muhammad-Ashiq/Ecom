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

        public DbSet<Config> Configs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }

    }
}
