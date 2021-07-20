using MyLibrary.Utilities;
using Microsoft.EntityFrameworkCore;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreInventory.DAL.Interfaces;

namespace StoreInventory.DAL
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetProducts()
        {
            List<Product> products;
            using (var db = new StoreContext())
            {
                products = db.Products
                     .Include(p => p.Category)
                     .OrderBy(p => p.Category.Name)
                     .ThenBy(p => p.Name)
                     .ToList();
            }

            return products;
        }
        public void AddingProduct(DTO.Product newDtoProduct)
        {
            Product newModelProduct = MyMapper.Mapper(newDtoProduct, new Product());

            using (var db = new StoreContext())
            {
                db.Products.Add(newModelProduct);
                db.SaveChanges();
            }
        }
        
        public void EditingProduct(DTO.Product productToEdit)
        {
            using (var db = new StoreContext())
            {
                Product modelProduct = db.Products.Find(productToEdit.Id);
                MyMapper.Mapper(productToEdit, modelProduct);
                db.SaveChanges();
            }
        }

        public void DeletingProduct(int productId)
        {
            using (var db = new StoreContext())
            {
                db.Products.Find(productId);
                db.SaveChanges();
            }
        }

    }
}
