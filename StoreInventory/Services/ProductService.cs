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
using System.Threading.Tasks;

namespace StoreInventory.Services
{
    public class ProductService
    {
        private IProductRepository _productRepository;
        public Action<string, string> OkMessageBoxEvent;
        public Action<string, string> OkAndCancelMessageBoxEvent;
        public static bool UsersConfirmation { get; set; }
        public List<DTO.Product> AllProducts { get; private set; }
       
        public ProductService() { }
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            AllProducts = ConvertToDtoProducts(_productRepository.GetProducts()).ToList();
        }

        public void UpdateProduct(DTO.Product dtoProduct)
        {
            _productRepository.EditingProduct(dtoProduct);
        }

        public void DeleteProduct(int productId)
        {
            OkAndCancelMessageBoxEvent?.Invoke("Warning!","Are you sure you would like to delete this product!");
            if (UsersConfirmation)
                _productRepository.DeletingProduct(productId);
        }

        public void AddNewProduct(DTO.Product newProduct)
        {
            if (ValidProductToAdd(newProduct))
                _productRepository.AddingProduct(newProduct);
        }
        private bool ValidProductToAdd(DTO.Product newProduct)
        {
            if (string.IsNullOrWhiteSpace(newProduct.Name) || string.IsNullOrWhiteSpace(newProduct.Description)
                || string.IsNullOrWhiteSpace(newProduct.Category.Name) || newProduct.Price == 0f )
            {
                OkMessageBoxEvent?.Invoke("Missing Details!","You forgot to fill in or select one of the boxes!");
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
