using MyLibrary.Extentions;
using StoreInventory.DAL;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StoreInventory.Services
{
    public class ProductSearchService
    {
        private IProductRepository _productRepository;
        public ObservableCollection<DTO.Product> AllProducts { get; private set; }


        public ProductSearchService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            AllProducts = ToDTOProductList( _productRepository.GetProducts()).ToObservableCollection();
        }
        public ObservableCollection<DTO.Product> SearchProducts(string searchInput)
        {
            int id;
            var isNumber = int.TryParse(searchInput, out id);
            var searchList = (isNumber) ? AllProducts.Where(mp => mp.Id == id).ToList() : AllProducts
                                                   .Where(mp => mp.Category.Name
                                                   .Contains(searchInput.Trim(), StringComparison.OrdinalIgnoreCase))
                                                   .OrderBy(mp => mp.Name)
                                                   .ToList();
                                                   
            if (searchList.Count == 0)
                searchList = AllProducts.Where(mp => mp.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase)).ToList();

            return searchList.ToObservableCollection();
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
