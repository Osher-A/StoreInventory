using Microsoft.EntityFrameworkCore;
using StoreInventory.Migrations.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Model
{
    public class StoreContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<StockIn> StockIns { get; set; }
        public virtual DbSet<OrderProduct> OrdersProducts { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database=Store;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrdersProducts)
                .WithOne(so => so.Order);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasDefaultValue(0f);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
