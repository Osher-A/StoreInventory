using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using StoreInventory.Interfaces;
using StoreInventory;
using StoreInventory.Services.ProductControllerServices;
using StoreInventory.DTO;

namespace UnitTests.ServicesTests
{
    [TestFixture]
    public class ProductDataServiceTests
    {
        private Mock<IProductRepository> _mockRepository;
        private ProductDataService _productService;
        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockRepository.Setup(mr => mr.GetAllProducts()).Returns(ModelProducts());
            _productService = new ProductDataService(_mockRepository.Object);
        }

        [Test]
        public void AddNewProduct_IfNewPropertyIsValid_AddingProductMethodIsCalled()
        {
            // Arrange

            var newProduct = new StoreInventory.DTO.Product
            {
                Id = 1,
                Category = new StoreInventory.DTO.Category { Name = "Food" },
                Name = "Danish",
                Description = "Yummy Danish",
                Price = 1.50f
            };
             
            // Act
            _productService.AddNewProduct(newProduct);

            //Assert
            _mockRepository.Verify(mr => mr.AddingProduct(It.IsAny<Product>()));
        }

        [Test]
        [TestCase(0, "", "Bun", "Yummy Bun", 1.05f) ]
        [TestCase(0, "Home", "", "Large Clock", 12.05f)]
        [TestCase(0, "Food", "Coke", "", 1.50f)]
        [TestCase(0, "Home", "Bin", "Large Bin", 0)]

        public void AddNewProduct_IfNewPropertyHasNotBeenFullySet_ReturnFalse
            (int id,string categoryName, string name, string description, float price)
        {
            // Arrange
            var newProduct = new StoreInventory.DTO.Product
            {
                Id = id,
                Name = name,
                Description = description,
                Price = price,
                Category = new StoreInventory.DTO.Category { Name = categoryName }
            };

            // Act
            _productService.AddNewProduct(newProduct);

            //Assert
            _mockRepository.Verify(mr => mr.AddingProduct(It.IsAny<Product>()), Times.Never());

        }

        [Test]
        public void ExistingProduct_IfIdenticelToExistingProduct_ReturnsTrue()
        {
            //Arrange
            var existingProduct = new Product()
            {
                Id = 0,
                Category = new Category { Name = "Food" },
                Name = "Danish",
                Description = "Yummy Danish",
                Price = 1.50f
            };

            //Act
            var productExists = _productService.ExistingProduct(existingProduct);

            //Assert
            Assert.That(productExists, Is.True);
        }

        [Test]
        public void ExistingProduct_IfNewProduct_ReturnFalse()
        {
            //Arrange
            var newProduct = new Product()
            {
                Id = 0,
                Category = new Category { Name = "Food" },
                Name = "Danish",
                Description = "Large Danish",
                Price = 2.50f
            };

            //Act
           var productExists = _productService.ExistingProduct(newProduct);

            //Assert
            Assert.That(productExists == false);
        }

        private List<IProduct> ModelProducts()
        {
            return new List<IProduct>
            {
                new StoreInventory.Model.Product
                {
                 Id = 1,
                 Category = new StoreInventory.Model.Category{Name = "Food"},
                 Name = "Danish",
                 Description = "Yummy Danish",
                 Price = 1.50f
                },
                new StoreInventory.Model.Product
                {
                 Id = 2,
                 Category = new StoreInventory.Model.Category{Name = "Clothes"},
                 Name = "Shirt",
                 Description = "Smart Shirt",
                 Price = 11.50f
                },
                new StoreInventory.Model.Product
                {
                 Id = 3,
                 Category = new StoreInventory.Model.Category{Name = "Home"},
                 Name = "Mirror",
                 Description = "Big Mirror",
                 Price = 23.00f
                },
            };
        }

    }
}
