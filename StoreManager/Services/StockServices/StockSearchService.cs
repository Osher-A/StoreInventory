using MyLibrary.Extentions;
using MyLibrary.Utilities;
using StoreManager.DAL;
using StoreManager.Interfaces;
using StoreManager.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StoreManager.Services.StockServices
{
    public class StockSearchService 
    {
        private IStockRepository _stockRepository;

        public StockSearchService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public ObservableCollection<DTO.Stock> AllStockProducts()
        {
            var modelStocks = _stockRepository.GetAllStocks();
            return ConvertToDtoStock(modelStocks).OrderBy(s => s.Product.Category.Name).ToObservableCollection();
        }

        public ObservableCollection<Stock> GetLowInStockProducts()
        {
            return AllStockProducts().Where(s => s.StockStatus == Enums.StockStatus.LowInStock).ToObservableCollection();
        }

        public ObservableCollection<Stock> GetOutOfStockProducts()
        {
            return AllStockProducts().Where(s => s.StockStatus == Enums.StockStatus.OutOfStock).ToObservableCollection();
        }
        public ObservableCollection<DTO.Stock> SearchStock(string searchInput, IProductRepository productRepository)
        {
            var productService = new ProductSearchService(productRepository);
            var searchProducts = productService.SearchProducts(searchInput);

            return ConvertToDtoStock(_stockRepository.SearchStocks(searchProducts.ToList<IProduct>()).ToList()).ToObservableCollection();
        }


        private IEnumerable<DTO.Stock> ConvertToDtoStock(List<IStock> modelStocks)
        {
            foreach (var stock in modelStocks)
            {
                var dtoStock = (DTO.Stock)(Model.Stock)stock;
                yield return dtoStock;
            }
        }
    }
}
