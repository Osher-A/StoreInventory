using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Migrations.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
         builder.HasMany(p => p.StockIns)
                .WithOne(si => (Product)si.Product);

         builder.HasMany(p => p.OrdersProducts)
                .WithOne(so => (Product)so.Product);

         builder.HasOne(p => (Stock)p.Stock)
                .WithOne(s => (Product)s.Product);
        }
    }
}
