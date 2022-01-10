using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using StoreManager.Model;
using StoreManager.Interfaces;
using Moq;
using StoreManager.Services.StockServices;
using System.Collections.ObjectModel;
using System.Drawing.Text;

namespace UnitTests.ServicesTests.ProductControllerServiceTests
{
    [TestFixture]
    class ProductSearchServiceTests
    {
        private Mock<IProductRepository> _productRepository;
        private ProductSearchService _searchService;

        [SetUp]
        public void SetUp()
        {
            _productRepository = new Mock<IProductRepository>();
            _productRepository.Setup(pr => pr.GetAllProducts()).Returns(ModelProducts());
            _searchService = new ProductSearchService(_productRepository.Object);
        }

        [Test]
        public void AllProducts_WhenSelected_ReturnsAConvertedListOfDTOProducts()
        {
            //Arrange

            //Act
            var dtoProducts = _searchService.AllProducts;

            // Assert
            Assert.That(dtoProducts, Is.TypeOf<ObservableCollection<StoreManager.DTO.Product>>());
            Assert.That(dtoProducts.Count == 5);
        }

        [Test]
        public void SearchProducts_SearchInputIsNumber_ReturnProductWithThatId()
        {
            var searchProduct = _searchService.SearchProducts("3");
            Assert.That(searchProduct[0].Name == "Mirror");
        }

        [Test]
        public void SearchProducts_SearchInputIsPartOfProductName_ReturnsProductsWithThatProductName()
        {
            var searchProduct = _searchService.SearchProducts("Ish");
            Assert.That(searchProduct[0].Name == "Danish");
        }

        [Test]
        public void SearchProducts_SearchInputMatchesCategoryAndPropertyName_ReturnsBothDistinctly()
        {
            var searchProduct = _searchService.SearchProducts("s");
            Assert.That(searchProduct.Count == 3);
        }
        [Test]
        public void SearchProducts_SearchInputNotMatchingAnything_ReturnsEmptyList()
        {
            var searchProduct1 = _searchService.SearchProducts("23");
            var searchProduct2 = _searchService.SearchProducts("Hammer");
            Assert.That(searchProduct1.Count == 0);
            Assert.That(searchProduct2.Count == 0);
        }

        private List<IProduct> ModelProducts()
        {

            return new List<IProduct>
            {
                new StoreManager.Model.Product
                {
                 Id = 1,
                 Category = new StoreManager.Model.Category{Name = "Food"},
                 Name = "Danish",
                 Description = "Yummy Danish",
                 Price = 1.50f
                },
                new StoreManager.Model.Product
                {
                 Id = 2,
                 Category = new StoreManager.Model.Category{Name = "Clothes"},
                 Name = "Hat",
                 Description = "Big hat",
                 Price = 11.50f
                },
                new StoreManager.Model.Product
                {
                 Id = 2,
                 Category = new StoreManager.Model.Category{Name = "Clothes"},
                 Name = "Socks",
                 Description = "Blue Socks",
                 Price = 11.50f
                },
                new StoreManager.Model.Product
                {
                 Id = 3,
                 Category = new StoreManager.Model.Category{Name = "Home"},
                 Name = "Mirror",
                 Description = "Big Mirror",
                 Price = 23.00f
                },
                new StoreManager.Model.Product
                {
                 Id = 1,
                 Category = new StoreManager.Model.Category{Name = "Food"},
                 Name = "Pizza food",
                 Description = "Delicious stuff!",
                 Price = 8.50f
                }
            };

        }
       
    }
}
