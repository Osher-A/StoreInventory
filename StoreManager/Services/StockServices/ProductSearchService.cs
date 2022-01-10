using MyLibrary.Extentions;
using StoreManager.DAL;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StoreManager.Services.StockServices
{
    public class ProductSearchService
    {
        private IProductRepository _productRepository;
        public ObservableCollection<DTO.Product> AllProducts { get; private set; }


        public ProductSearchService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            AllProducts = ToDTOProductList( _productRepository.GetAllProducts()).ToObservableCollection();
        }
        public ObservableCollection<DTO.Product> SearchProducts(string searchInput)
        {
            int id; List<DTO.Product> searchList;
            var isNumber = int.TryParse(searchInput, out id);
            searchList = (isNumber) ? AllProducts.Where(mp => mp.Id == id).ToList() : AllProducts
                                                   .Where(mp => mp.Category.Name
                                                   .Contains(searchInput.Trim(), StringComparison.OrdinalIgnoreCase))
                                                   .OrderBy(mp => mp.Name)
                                                   .ToList();
                                                   
           searchList.AddRange(AllProducts.Where(mp => mp.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase)).ToList());

           return searchList.Distinct().ToObservableCollection();
        }

        private IEnumerable<DTO.Product> ToDTOProductList(List<IProduct> modelProducts)
        {
            foreach (var product in modelProducts)
            {
                yield return (DTO.Product)(Model.Product)product;
            }
        }
    }
}
