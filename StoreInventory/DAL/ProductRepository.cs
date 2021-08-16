using MyLibrary.Utilities;
using Microsoft.EntityFrameworkCore;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoreInventory.Interfaces;

namespace StoreInventory.DAL
{
    public class ProductRepository : IProductRepository
    {
        public List<IProduct> GetProducts()
        {
            List<IProduct> products;
            using (var db = new StoreContext())
            {
                products = db.Products
                     .Include(p => p.Category)
                     .OrderBy(p => p.Category.Name)
                     .ThenBy(p => p.Name).ToList<IProduct>();
            }

            return products;
        }
        public void AddingProduct(IProduct newusersProduct)
        {
            Product newModelProduct = new Product();
            MapUsersProductToModel(newModelProduct, newusersProduct);

            using (var db = new StoreContext())
            {
                db.Products.Add(newModelProduct);
                db.SaveChanges();
            }
        }
        
        public void EditingProduct(IProduct usersProduct)
        {
            using (var db = new StoreContext())
            {
                Product modelProduct = db.Products.Find(usersProduct.Id);
                MapUsersProductToModel(modelProduct, usersProduct);
                db.SaveChanges();
                    
            }
        }
        private void MapUsersProductToModel(Model.Product modelProduct, IProduct usersProduct)
        {
            modelProduct.Name = usersProduct.Name;
            modelProduct.Description = usersProduct.Description;
            modelProduct.Price = usersProduct.Price;
            modelProduct.Image = usersProduct.Image;
            modelProduct.CategoryId = usersProduct.CategoryId;
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
