using Moq;
using NUnit.Framework;
using StoreInventory.Interfaces;
using StoreInventory.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using StoreInventory.DTO;

namespace UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    internal class OrderDataServiceTests
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<IOrderProductRepository> _orderProductRepository;
        private IOrder _mockNewOrder;
        private List<BasketItem> _mockBasketItems;
        private List<ICustomer> _mockDbCustomers;
        private OrderDataService _orderDataService;

        [SetUp]
        public void SetUp()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _customerRepository = new Mock<ICustomerRepository>();
            _customerRepository.Setup(cr => cr.GetCustomers()).Returns(_mockDbCustomers);
            _orderDataService = new OrderDataService(_orderRepository.Object, _customerRepository.Object,
                _orderProductRepository.Object, _mockNewOrder, _mockBasketItems);

            _mockNewOrder = new Order();
            SetBasketItems();
            SetDbCustomers();

        }
        [Test]
        public void SaveOrderDetails_WhenAllCustomersDetailsAreNullAndFullyPaid_DoNotAddOrUpdateCustomerInRepo()
        {
            //Set up
            _mockNewOrder.Total = 10; _mockNewOrder.AmountPaid = 10;
            _mockNewOrder.Customer = new Customer();
            
            //Act
            _orderDataService.SaveOrderDetails();

            //Assert
        }
       
        [Test]
        public void SaveOrderDetails_WhenPartOfDetailsNotNullAndCanBeMatchedToExistingCustomer_UpdateCustomerInRepo()
        {

        }
        [Test]
        public void SaveOrderDetails_WhenPartOfDetailsNotNullAndCantBeMatchedToExistingCustomer_AddNewCustomerInRepo()
        {

        }

        private void SetBasketItems()
        {
            _mockBasketItems = new List<BasketItem>();
            _mockBasketItems.Add(new BasketItem { Product = new Product { Id = 1, Name = "Shirt"}, Quantity = 1 });
            _mockBasketItems.Add(new BasketItem { Product = new Product { Id = 2, Name = "Pie"}, Quantity = 2 });
        }

        private void SetDbCustomers()
        {
            _mockDbCustomers = new List<ICustomer>();
            _mockDbCustomers.Add(new Customer()
            {
                Email = "oa@gmail.com",
                FirstNames = "Osher Aharon",
                LastName = "Moscovitch",
                Address = new Address
                {
                    House = "10",
                    Street = "Sedgley Avenue",
                    Zip = "M25 0LS",
                    City = "Manchester"
                }
            });
            _mockDbCustomers.Add(new Customer()
            {
                Email = "be@gmail.com",
                FirstNames = "Ben",
                LastName = "Delta",
                Address = new Address
                {
                    House = "1",
                    Street = "Sedgley Park",
                    Zip = "M24 0LS",
                    City = "Manchester"
                }
            }); 
            _mockDbCustomers.Add(new Customer()
            {
                Email = "bo@gmail.com",
                FirstNames = "Bob",
                LastName = "Kanter",
                Address = new Address
                {
                    House = "20",
                    Street = "Richman Avenue",
                    Zip = "M25 0LP",
                    City = "Manchesters"
                }
            });

        }
        
    }
}
