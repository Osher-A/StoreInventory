using MyLibrary.Extentions;
using MyLibrary.Utilities;
using StoreInventory.DAL;
using StoreInventory.Interfaces;
using StoreInventory.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StoreInventory.Services
{
    public class StockService
    {
        private ICategoryRepository _categoryRepos;
        private IStockRepository _stockRepository;
        public ObservableCollection<Stock> GetStocks { get; private set; }

        public StockService(IStockRepository stockRepository, ICategoryRepository categoryRepository)
        {
            _stockRepository = stockRepository;
            _categoryRepos = categoryRepository;
            GetStocks = GetAllStocks();
        }

        public ObservableCollection<Stock> GetLowInStockProducts()
        {
            return GetStocks.Where(s => s.StockStatus == Enums.StockStatus.LowInStock).ToObservableCollection();
        }

        public ObservableCollection<Stock> GetOutOfStockProducts()
        {
            return GetStocks.Where(s => s.StockStatus == Enums.StockStatus.OutOfStock).ToObservableCollection();
        }
        public ObservableCollection<DTO.Stock> SearchStocks(string searchInput, IProductRepository productRepository)
        {
            var productService = new ProductSearchService(productRepository);
            var searchProducts = productService.SearchProducts(searchInput);

            return ConvertToDtoStock(_stockRepository.SearchStocks(searchProducts.ToList<IProduct>()).ToList()).ToObservableCollection();
        }

        public ObservableCollection<DTO.Category> GetCategories()
        {
            return Categories().ToObservableCollection();
        }
        private ObservableCollection<DTO.Stock> GetAllStocks()
        {
            var modelStocks = _stockRepository.GetAllStocks();
            return ConvertToDtoStock(modelStocks).OrderBy(s => s.Product.Category.Name).ToObservableCollection();
        }

        private IEnumerable<DTO.Stock> ConvertToDtoStock(List<IStock> modelStocks)
        {
            foreach (var stock in modelStocks)
            {
                var dtoStock = (DTO.Stock)(Model.Stock)stock;
                yield return dtoStock;
            }
        }
        private IEnumerable<DTO.Category> Categories()
        {
            var modelcategories = _categoryRepos.GetCategories();
            foreach (var category in modelcategories)
            {
                DTO.Category dtoCategory = (DTO.Category)(Model.Category)category;
                yield return dtoCategory;
            }
        }
    }
}
