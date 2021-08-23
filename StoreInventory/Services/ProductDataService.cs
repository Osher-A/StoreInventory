using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreInventory.Services
{
    public class ProductDataService
    {
        private IProductRepository _productRepository;
        public static Action<string, string> OkMessageBoxEvent;
        public static Func<string, string, Task<bool>> OkAndCancelMessageBoxEvent;
        public static bool UsersConfirmation { get; set; }

        public ProductDataService() { }
        public ProductDataService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void AddNewProduct(IProduct newProduct)
        {
            if (ValidProductToAdd(newProduct))
                _productRepository.AddingProduct(newProduct);
        }

        public void UpdateProduct(IProduct usersProduct)
        {
            _productRepository.EditingProduct(usersProduct);
        }

        public async Task DeleteProduct(int productId)
        {
            var result = OkAndCancelMessageBoxEvent?.Invoke("Warning!","Are you sure you would like to delete this product!");
            if (await result)
                _productRepository.DeletingProduct(productId);
        }

        public bool ExistingProduct(IProduct newProduct)
        {
            var allProducts = (List<DTO.Product>)ConvertToDtoProducts(_productRepository.GetProducts());

            if (newProduct.Id != 0)
                return true;

            if (!string.IsNullOrWhiteSpace(newProduct.Name) && !string.IsNullOrWhiteSpace(newProduct.Category.Name))
            {
                if (allProducts.Exists(p => string.Equals(p.Category.Name.Trim(), newProduct.Category.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                 && allProducts.Exists(p => string.Equals(p.Name.Trim(), newProduct.Name.Trim(), StringComparison.OrdinalIgnoreCase))
                 && allProducts.Exists(p => string.Equals(p.Description.Trim(), newProduct.Description.Trim(), StringComparison.OrdinalIgnoreCase)))
                    return true;
            }

            return false;
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
