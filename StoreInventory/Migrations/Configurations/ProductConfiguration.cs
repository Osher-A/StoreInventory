using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Migrations.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
         builder.HasMany(p => p.StockIns)
                .WithOne(si => si.Product);

         builder.HasMany(p => p.OrdersProducts)
                .WithOne(so => so.Product);

         builder.HasOne(p => p.Stock)
                .WithOne(s => s.Product);
        }
    }
}
