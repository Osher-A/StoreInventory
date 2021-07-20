using MyLibrary.Extentions;
using MyLibrary.Utilities;
using StoreInventory.DAL;
using StoreInventory.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MahApps.Metro;
using StoreInventory.DAL.Interfaces;

namespace StoreInventory.Services
{
    public class ProductService
    {
        private IProductRepository _productRepository;
        public static Action<string> MessageBoxEvent;

        public List<DTO.Product> AllProducts { get; private set; }
       
        public ProductService() { }
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            AllProducts = ConvertToDtoProducts(_productRepository.GetProducts()).ToList();
        }

        public bool ValidProductToAdd(DTO.Stock newStock)
        {
            if (string.IsNullOrWhiteSpace(newStock.Product.Name) || string.IsNullOrWhiteSpace(newStock.Product.Description)
                || string.IsNullOrWhiteSpace(newStock.Product.Category.Name) || newStock.Product.Price == 0f || newStock.QuantityInStock == 0 )
            {
                MessageBoxEvent?.Invoke("You forgot to fill in or select one of the boxes!");
                return false;
            }
            else
                return true;
        }
       
        public bool ExistingProduct(DTO.Product newProduct)
        {
            if (newProduct.Id != 0)
                return true;

            if (newProduct.Name != null)
            {
                if (AllProducts.Exists(p => string.Equals(p.Category.Name.Trim(), newProduct.Category.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                 && AllProducts.Exists(p => string.Equals(p.Name.Trim(), newProduct.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                 && AllProducts.Exists(p => string.Equals(p.Description.Trim(), newProduct.Description.Trim(), StringComparison.OrdinalIgnoreCase))
                  && AllProducts.Exists(p => p.Price == newProduct.Price))
                      return true;
            }

             return false;
        }
        private IEnumerable<DTO.Product> ConvertToDtoProducts(List<Model.Product> products)
        {
            var dtoProducts = new List<DTO.Product>();
            foreach (var product in products)
            { 
                //This cast has been made possible through the explicit operator in the product class.

                DTO.Product dtoProduct = (DTO.Product)product; 
                dtoProducts.Add(dtoProduct);
            }
            return dtoProducts;
        }

       
    }
}
