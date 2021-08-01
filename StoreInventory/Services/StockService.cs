using MyLibrary.Extentions;
using MyLibrary.Utilities;
using StoreInventory.DAL;
using StoreInventory.DAL.Interfaces;
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

        public StockService(IStockRepository stockRepository, ICategoryRepository categoryRepository)
        {
            _stockRepository = stockRepository;
            _categoryRepos = categoryRepository;
        }

        public ObservableCollection<DTO.Stock> GetStocks()
        {
           var modelStocks = _stockRepository.GetStocks();
           return ConvertToDtoStock(modelStocks).OrderBy(s => s.Product.Category.Name).ToObservableCollection();
        }

        public ObservableCollection<DTO.Category> GetCategories()
        {
            return Categories().ToObservableCollection();
        }

        private IEnumerable<DTO.Stock> ConvertToDtoStock(List<Model.Stock> modelStocks)
        {
            foreach (var stock in modelStocks)
            {
                var dtoStock = (DTO.Stock)stock;
                yield return dtoStock;
            }
        }
        private IEnumerable<DTO.Category> Categories()
        {
            var modelcategories = _categoryRepos.GetCategories();
            foreach (var category in modelcategories)
            {
                DTO.Category dtoCategory = (DTO.Category)category;
                yield return dtoCategory;
            }
        }
    }
}
