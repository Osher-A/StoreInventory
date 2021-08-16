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
        private List<IProduct> _modelProducts;

        public ProductSearchService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _modelProducts = _productRepository.GetProducts();
        }
        public ObservableCollection<DTO.Product> SearchProducts(string searchInput)
        {
            int id;
            var isNumber = int.TryParse(searchInput, out id);
            var searchList = (isNumber)? _modelProducts.Where(mp => mp.Id == id).ToList(): _modelProducts
                                                   .Where(mp => mp.Category.Name                                       
                                                   .Contains(searchInput.Trim(), StringComparison.OrdinalIgnoreCase))
                                                   .OrderBy(mp => mp.Name)
                                                   .ToList();
            if (searchList.Count == 0)
                searchList = _modelProducts.Where(mp => mp.Name.Contains(searchInput, StringComparison.OrdinalIgnoreCase)).ToList();

            return ToDTOProductList(searchList).ToList().ToObservableCollection();
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
