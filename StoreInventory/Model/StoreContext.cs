using Microsoft.EntityFrameworkCore;
using StoreInventory.Extentions;
using StoreInventory.Interfaces;
using StoreInventory.Migrations.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Database=Store;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => (Category)p.Category);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => (Customer)o.Customer);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrdersProducts)
                .WithOne(so => (Order)so.Order);

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasDefaultValue(0f);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });
            modelBuilder.Entity<Stock>()
                .Ignore(s => s.StockStatus);

            modelBuilder.Entity<Customer>()
                 .HasOne(c => (Address)c.Address)
                 .WithOne(a => (Customer)a.Customer)
                 .HasForeignKey("Customer", "AddressId");

            modelBuilder.Entity<Address>()
                 .HasOne(a => (Customer)a.Customer)
                 .WithOne(c => (Address)c.Address)
                 .HasForeignKey("Address", "CustomerId");


            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        
    }
}
