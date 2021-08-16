using Moq;
using NUnit.Framework;
using StoreInventory.DAL;
using StoreInventory.Interfaces;
using StoreInventory.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UnitTests.ServicesTests
{
    [TestFixture]
    public class StockServiceTests
    {
        private Mock<IStockRepository> _mockRepository = new Mock<IStockRepository>();
        private Mock<ICategoryRepository> _mockCategoryRepos = new Mock<ICategoryRepository>();

        [Test]
        public void GetStocks_WhenCalled_ReturnsConvertedListOfDTOStocksOrderedByCategories()
        {
            //Arrange
            _mockRepository.Setup(mr => mr.GetAllStocks()).Returns(ModelStocks());
            _mockCategoryRepos.Setup(cr => cr.GetCategories()).Returns(Categories());
            var stockService = new StockService(_mockRepository.Object, _mockCategoryRepos.Object);

            //Act
            var dtoStocks = stockService.GetStocks;

            //Assert
            Assert.That(dtoStocks, Is.TypeOf<ObservableCollection<StoreInventory.DTO.Stock>>());
            Assert.That(dtoStocks[0].Product.Category.Name == "Clothes");
        }

        private List<IStock> ModelStocks()
        {
            return new List<IStock>
            {
                new StoreInventory.Model.Stock
                {
                    Id = 1,
                    QuantityInStock = 10,
                    Product = new StoreInventory.Model.Product
                    {
                        Id = 1,
                        CategoryId = 1,
                        Category = new StoreInventory.Model.Category{Id = 1, Name = "Food"},
                        Name = "Danish",
                        Description = "Yummy Danish",
                        Price = 1.50f
                    }
                },
                new StoreInventory.Model.Stock
                {
                    Id = 2,
                    QuantityInStock = 5,
                    Product = new StoreInventory.Model.Product
                    {
                        Id = 2,
                        CategoryId = 2,
                        Category = new StoreInventory.Model.Category{Id = 2, Name = "Clothes"},
                        Name = "Shirt",
                        Description = "Smart Shirt",
                        Price = 11.50f
                    }
                },
                new StoreInventory.Model.Stock
                {
                    Id = 3,
                    QuantityInStock = 15,
                    Product = new StoreInventory.Model.Product
                    {
                        Id = 5,
                        CategoryId = 3,
                        Category = new StoreInventory.Model.Category{Id = 3, Name = "Home"},
                        Name = "Mirror",
                        Description = "Big Mirror",
                        Price = 23.00f
                    }
                }
            };
        }
        private List<ICategory> Categories()
        {
            return new List<ICategory>
            {
               new StoreInventory.Model.Category{Id = 1, Name = "Food"},
               new StoreInventory.Model.Category{Id = 2, Name = "Clothes"},
               new StoreInventory.Model.Category{Id = 3, Name = "Home"}
            };
        }
    }
}
