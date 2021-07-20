using MyLibrary.Extentions;
using MyLibrary.Utilities;
using StoreInventory.DAL;
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
        private CategoryRepository _categoryRepos = new CategoryRepository();
        
        public ObservableCollection<DTO.Stock> GetStocks()
        {
            var stockRepository = new StockRepository();
            var modelStocks = stockRepository.GetStocks();
           return ConvertToDtoStock(modelStocks).OrderBy(s => s.Product.Name).ToObservableCollection();
        }

        public ObservableCollection<DTO.Category> GetCategories()
        {
            return Categories().ToObservableCollection();
        }
        private IEnumerable<DTO.Stock> ConvertToDtoStock(List<Model.Stock> modelStocks)
        {
            foreach (var stock in modelStocks)
            {
                var dtoStock = new DTO.Stock();
                var dtoProduct = MyMapper.Mapper(stock.Product, new DTO.Product ());
                dtoStock.Product = dtoProduct;
                dtoStock.Product.Category = GetCategory(stock.Product.CategoryId);
                dtoStock.QuantityInStock = stock.QuantityInStock;
                yield return dtoStock;
            }
        }

        private DTO.Category GetCategory(int categoryId)
        {
            DTO.Category dtoCategory = (DTO.Category)_categoryRepos.GetCategory(categoryId);
            return dtoCategory;
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
