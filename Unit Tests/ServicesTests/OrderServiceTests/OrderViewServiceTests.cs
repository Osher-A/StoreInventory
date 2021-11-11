using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Collections.ObjectModel;
using StoreInventory.DAL;
using StoreInventory.Model;
using StoreInventory.Interfaces;
using StoreInventory.Services.OrderServices;
using System.Linq;

namespace UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    public class OrderViewServiceTests
    {
        private Mock<IStockRepository> _stockRepository;
        private ShoppingBasketService _shoppingBasketService;
        private OrderViewService _orderViewService;
        [SetUp]
        public void SetUp()
        {
            _stockRepository = new Mock<IStockRepository>();
            _stockRepository.Setup(sr => sr.GetAllStocks()).Returns(GetListOfStocks());
            _shoppingBasketService = new ShoppingBasketService(_stockRepository.Object);
            _orderViewService = new OrderViewService(_shoppingBasketService);
        }

        [Test]
        public void  GetSearchProducts_WhenSearchBoxEmptyAndCategoryBoxAsDefault_ReturnAllProductsInOrder()
        {
            var result = _orderViewService.GetSearchProducts("", 1);

            Assert.That(result.Count, Is.EqualTo(GetListOfStocks().Count));
            Assert.That(result.First().Product.Category.Name == "Clothes" && result.First().Product.Name == "Jumper");
        }

        [Test]
        public void GetSearchProducts_WhenSearchBoxEmptyAndCategoryBoxSelected_ReturnAllCategoryProducts()
        {
            var result = _orderViewService.GetSearchProducts("", 3);

            Assert.That(result.Count == 2);
            Assert.That(result.First().Product.Name == "Danish");
        }

        [Test]
        public void GetSearchProducts_WhenSearchBoxHasInput_AndCategoryBoxAsDefault_ReturnAllProductsThatContainThoseCharacters()
        {
            var result = _orderViewService.GetSearchProducts("Pot", 1);

            Assert.That(result.Count == 2);
        }

        [Test]
        public void GetSearchProducts_WhenSearchBoxHasInput_AndCategoryBoxSelected_ReturnsCategoryProductsThatContainThoseCharacters()
        {
            var result = _orderViewService.GetSearchProducts("Pot", 3);

            Assert.That(result.Count == 1);
        }
        private List<IStock> GetListOfStocks()
        {
           return new List<IStock>()
            {
                new Stock { Product = new Product { Name = "Shirt", Price = 10.20f, Description = "Iron free shirt" , CategoryId = 4, Category = new Category {Id = 4 ,Name = "Clothes"} }, QuantityInStock = 10},
                 new Stock { Product = new Product { Name = "Pot", Price = 7.50f, Description = "Silver Pot" , CategoryId = 2, Category = new Category {Id = 2, Name = "Home"} }, QuantityInStock = 15},
                new Stock { Product = new Product { Name = "Potato chips", Price = 1.60f, Description = "Salty crisps" , CategoryId = 3, Category = new Category {Id = 3, Name = "Food"} }, QuantityInStock = 30},
                new Stock { Product = new Product { Name = "Jumper", Price = 15.20f, Description = "Cotton jumper" , CategoryId = 4, Category = new Category {Id = 4, Name = "Clothes"} }, QuantityInStock = 12},
                new Stock { Product = new Product { Name = "Danish", Price = 1.20f, Description = "Chocolate Danish" , CategoryId = 3, Category = new Category {Id = 3, Name = "Food"} }, QuantityInStock = 25},
                new Stock { Product = new Product { Name = "Vase", Price = 7.00f, Description = "Hand made vase" , CategoryId = 2, Category = new Category {Id = 2, Name = "Home"} }, QuantityInStock = 10},
                new Stock { Product = new Product { Name = "Socks", Price = 4.20f, Description = "Pair of warm Socks" , CategoryId = 4, Category = new Category {Id = 4, Name = "Clothes"} }, QuantityInStock = 6}, 
                new Stock { Product = new Product { Name = "Trousers", Price = 18.80f, Description = "Black suit trousers" , CategoryId = 4, Category = new Category {Id = 4, Name = "Clothes"} }, QuantityInStock = 8},
            };
        }

    }
}
