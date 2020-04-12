using System;
using Microsoft.EntityFrameworkCore;

namespace Product_Services.Models
{
    public class Product_Context : DbContext
    {
        public Product_Context(DbContextOptions<Product_Context> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Product_Category> Product_Categories { get; set; }
        public DbSet<Product_Image> Product_Images { get; set; }
        public DbSet<Image> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product_Image>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
            builder.Entity<Image>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
            builder.Entity<Product>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
            builder.Entity<Discount>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
            builder.Entity<Product_Category>()
           .Property(p => p.Id)
           .ValueGeneratedOnAdd();
            builder.Entity<Category>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        }
    }

}