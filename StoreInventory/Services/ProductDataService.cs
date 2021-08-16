using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;

namespace StoreInventory.Services
{
    public class ProductDataService
    {
        private IProductRepository _productRepository;
        public Action<string, string> OkMessageBoxEvent;
        public Action<string, string> OkAndCancelMessageBoxEvent;
        public static bool UsersConfirmation { get; set; }
        
       public List<DTO.Product> AllProducts { get; private set; }
        public ProductDataService() { }
        public ProductDataService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            AllProducts = (List<DTO.Product>)ConvertToDtoProducts(_productRepository.GetProducts()) ;
        }

        public void UpdateProduct(IProduct usersProduct)
        {
            _productRepository.EditingProduct(usersProduct);
        }

        public void DeleteProduct(int productId)
        {
            OkAndCancelMessageBoxEvent?.Invoke("Warning!","Are you sure you would like to delete this product!");
            if (UsersConfirmation)
                _productRepository.DeletingProduct(productId);
        }

        public void AddNewProduct(IProduct newProduct)
        {
            if (ValidProductToAdd(newProduct))
                _productRepository.AddingProduct(newProduct);
        }
        private bool ValidProductToAdd(IProduct newProduct)
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
        public bool ExistingProduct(IProduct newProduct)
        {
            if (newProduct.Id != 0)
                return true;

            if (!string.IsNullOrWhiteSpace(newProduct.Name) && !string.IsNullOrWhiteSpace(newProduct.Category.Name))
            {
                if (AllProducts.Exists(p => string.Equals(p.Category.Name.Trim(), newProduct.Category.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                 && AllProducts.Exists(p => string.Equals(p.Name.Trim(), newProduct.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                 && AllProducts.Exists(p => string.Equals(p.Description.Trim(), newProduct.Description.Trim(), StringComparison.OrdinalIgnoreCase)))
                      return true;
            }

             return false;
        }
        private IEnumerable<DTO.Product> ConvertToDtoProducts(List<IProduct> products)
        {
            var dtoProducts = new List<DTO.Product>();
            foreach (var product in products)
            {
                Model.Product modelProduct = (Model.Product)product;

                //This cast has been made possible through the explicit operator in the product class.
                DTO.Product dtoProduct = (DTO.Product)modelProduct; 
                dtoProducts.Add(dtoProduct);
            }
            return dtoProducts;
        }

       
    }
}
