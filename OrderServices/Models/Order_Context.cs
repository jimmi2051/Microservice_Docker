using System;
using Microsoft.EntityFrameworkCore;

namespace Order_servies.Models
{ 
    public class Order_Context : DbContext
    {
        public Order_Context(DbContextOptions<Order_Context> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_detail> Order_details { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product> Products{get;set;}
        public DbSet<Cart> Carts{get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
        builder.Entity<Order_detail>().HasKey(table => new {
            table.Id, table.ProductID
        });
        builder.Entity<Order>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();
        builder.Entity<Cart>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();
         builder.Entity<Account>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();
         builder.Entity<Product>()
        .Property(p => p.Id)
        .ValueGeneratedOnAdd();
        }
        
    }
    
}