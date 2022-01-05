using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using StoreManager.Interfaces;
using StoreManager.Model;
using StoreManager.Services.OrderServices;
using System.Linq;

namespace UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    public class ShoppingBasketServiceTests
    {
        private Mock<IStockRepository> _stockRepository;
        private ShoppingBasketService _shoppingBasketService;
        private Product _product;
        [SetUp]
        public void SetUp()
        {
            _stockRepository = new Mock<IStockRepository>();
            _stockRepository.Setup(sr => sr.GetAllStocks()).Returns(GetListOfStocks);
            _shoppingBasketService = new ShoppingBasketService(_stockRepository.Object);
            _product = new Product() { Id = 1, Name = "Shirt" , Price = 10.20f};
        }

        [Test]
        public void AddToOrder_WhenAddingNewProductWithOutSpecifieingQuantity_BasketItemsGetUpdatedItsQuantityIsSetWithOneAddStockQuantityIsReduced()
        {
            _shoppingBasketService.AddToOrder(_product, 0);

            Assert.That(_shoppingBasketService.BasketItems.Count == 1);
            Assert.That(_shoppingBasketService.BasketItems.ElementAt(0).Quantity == 1);
            Assert.That(_shoppingBasketService.StockRemaing.Single(sr => sr.ProductId == _product.Id).QuantityInStock == 9);
        }

        [Test]
        public void AddToOrder_WhenAddingExistingProductWithOutSpecifieingQuantity_BasketItemQuantityIncreasesByOne()
        {
            _shoppingBasketService.AddToOrder(_product, 0);
            _shoppingBasketService.AddToOrder(_product, 0);

            Assert.That(_shoppingBasketService.BasketItems.Count == 1);
            Assert.That(_shoppingBasketService.BasketItems.ElementAt(0).Quantity == 2);
        }
        [Test]
        public void AddToOrder_WhenChangingExistingProductsQuantity_BasketItemQuantityIsSetToNewQuantity()
        {
            _shoppingBasketService.AddToOrder(_product, 0);
            _shoppingBasketService.AddToOrder(_product, 5);

            Assert.That(_shoppingBasketService.BasketItems.ElementAt(0).Quantity == 5);
        }

        [Test]
        public void AddToOrder_WhenAddingProducts_EachItemIsSetWithARunningTotalAndTotalCostIsSet()
        {
            _shoppingBasketService.AddToOrder(_product, 2);
            _shoppingBasketService.AddToOrder(new Product { Id = 2, Name = "Pot" , Price = 7.50f }, 2);
            
            Assert.That(_shoppingBasketService.BasketItems.ElementAt(0).RunningTotal == 20.40f);
            Assert.That(_shoppingBasketService.BasketItems.ElementAt(1).RunningTotal == 35.40f);
            Assert.That(_shoppingBasketService.TotalCost == 35.40f);
        }

        [Test]
        public void RemoveItemFromBasket_WhenCalled_ItemGetsRemovedFromBasket_AndReturnedToStock()
        {
            _shoppingBasketService.AddToOrder(_product, 0);
            _shoppingBasketService.RemoveItemFromBasket(_product);

            Assert.That(_shoppingBasketService.BasketItems.Count == 0);
            Assert.That(_shoppingBasketService.StockRemaing.Single(sr => sr.ProductId == _product.Id).QuantityInStock == 10);
        }

        [Test]
        public void EmptyBasket_WhenCalled_BasketItemsEmpty()
        {
            _shoppingBasketService.AddToOrder(_product, 0);
            _shoppingBasketService.AddToOrder(new Product { Id = 2, Name = "Pot" , Price = 7.50f }, 0);
            _shoppingBasketService.EmptyBasket();

            Assert.That(_shoppingBasketService.BasketItems.Count == 0);
        }


        private List<IStock> GetListOfStocks()
        {
            return new List<IStock>()
            {
                new Stock { Product = new Product {Id = 1, Name = "Shirt", Price = 10.20f, Description = "Iron free shirt" , CategoryId = 4, Category = new Category {Id = 4 ,Name = "Clothes"} }, ProductId = 1, QuantityInStock = 10},
                 new Stock { Product = new Product {Id = 2, Name = "Pot", Price = 7.50f, Description = "Silver Pot" , CategoryId = 2, Category = new Category {Id = 2, Name = "Home"} },ProductId = 2, QuantityInStock = 15},
                new Stock { Product = new Product { Id = 3, Name = "Potato chips", Price = 1.60f, Description = "Salty crisps" , CategoryId = 3, Category = new Category {Id = 3, Name = "Food"} }, ProductId = 3, QuantityInStock = 30},
            };
        }
    }
}
