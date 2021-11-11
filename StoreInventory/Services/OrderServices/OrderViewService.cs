using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MyLibrary.Extentions;
using StoreInventory.DAL;
using StoreInventory.DTO;
using StoreInventory.Interfaces;
using StoreInventory.Services.ProductControllerServices;

namespace StoreInventory.Services.OrderServices
{
    public class OrderViewService
    {
         private CategoryService  _categoriesService = new CategoryService(new CategoryRepository());
        public ObservableCollection<DTO.Stock> AllProductsStocked { get; private set; }
        private ShoppingBasketService _shoppingBasketService;

        public OrderViewService(ShoppingBasketService shoppingBasketService)
        {
            _shoppingBasketService = shoppingBasketService;
            UpdateStockProducts();
        }
        
        public ObservableCollection<DTO.Stock> GetSearchProducts(string search, int categoryId)
        {
            UpdateStockProducts();

            if (categoryId == 1) // The First index == AllCategories
            {
                return AllProductsStocked.Where(s => s.Product.Name.Trim().Contains(search.Trim(), StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Product.Category.Name).ThenBy(s => s.Product.Name).ToObservableCollection();
            }
            else if (string.IsNullOrWhiteSpace(search))
            {
                return AllProductsStocked.Where(s => s.Product.Category.Id == categoryId).OrderBy(s => s.Product.Category.Name).ThenBy(s => s.Product.Name).ToObservableCollection();
            }
            else
            {
                return AllProductsStocked.Where(s => s.Product.Category.Id == categoryId && s.Product.Name.Trim().Contains(search.Trim(), StringComparison.CurrentCultureIgnoreCase)).OrderBy(s => s.Product.Category.Name).ThenBy(s => s.Product.Name).ToObservableCollection();
            }
        }

        public ObservableCollection<Category> GetCategories()
        {
            return _categoriesService.GetCategories();
        }

        public int GetCategoryId(string categoryName)
        {
            return GetCategories().SingleOrDefault(c => c.Name == categoryName).Id;
        }

        private IEnumerable<DTO.Stock> ConvertToDTOProducts(List<IStock> stocks)
        {
            foreach (var stock in stocks)
                yield return (DTO.Stock)(Model.Stock)stock;
        }

        private void UpdateStockProducts()
        {
            AllProductsStocked = ConvertToDTOProducts(_shoppingBasketService.StockRemaing).ToObservableCollection();
        }
    }
}
