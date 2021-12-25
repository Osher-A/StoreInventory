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
            _orderProductRepository = new Mock<IOrderProductRepository>();
            _mockNewOrder = new Order() { Customer = new Customer(), };
            SetBasketItems();
            SetDbCustomers();

            _customerRepository.Setup(cr => cr.GetCustomers()).Returns(_mockDbCustomers);
            _orderDataService = new OrderDataService(_orderRepository.Object, _customerRepository.Object,
                _orderProductRepository.Object, _mockNewOrder, _mockBasketItems);
        }

        [Test]
        [TestCase( "Darenth Rd", "N16 6PR")]
        public void SaveOrderDetails_WhenCustomersDetailsAreNotUnique_DoNotAddOrUpdateCustomerInRepo(string street, string zip)
        {
            //Set up
            _mockNewOrder.Customer.Address = new Address { Street = street, Zip = zip } ;
            
            //Act
            _orderDataService.SaveOrderDetails();

            //Assert
            _orderRepository.Verify(or => or.AddingOrder(_mockNewOrder), Times.Once());
            _customerRepository.Verify(cr => cr.UpdateCustomer(_mockNewOrder.Customer), Times.Never());
            _customerRepository.Verify(cr => cr.AddNewCustomer(_mockNewOrder.Customer), Times.Never());
        }

        [Test]
        public void SaveOrderDetails_WhenCustomersUniqueDetailsIsNotNullAndCanBeMatchedToExistingCustomer_UpdateCustomerInRepo()
        {
            //Set up
            _mockNewOrder.Customer.Email = "be@gmail.com" ;

            //Act
            _orderDataService.SaveOrderDetails();

            //Assert
            _orderRepository.Verify(or => or.AddingOrder(_mockNewOrder), Times.Once());
            
            _customerRepository.Verify(cr => cr.UpdateCustomer(_mockNewOrder.Customer), Times.Once());
            _customerRepository.Verify(cr => cr.AddNewCustomer(_mockNewOrder.Customer), Times.Never());

        }

        [Test]
        [TestCase("c.@gmail.com", null, "", null)]
        [TestCase(null, "12", "Queens Drive", "")]
        [TestCase("", "2", "", "M260PQ")]
        public void SaveOrderDetails_WhenUniqueDetailsNotNullButCantBeMatchedToExistingCustomer_AddNewCustomerInRepo(string email, string house, string street, string zip)
        {
            //Set up
            _mockNewOrder.Customer.Email = email;
            _mockNewOrder.Customer.Address = new Address()
            {
                House = house,
                Street = street,
                Zip = zip
            };
            
            //Act
            _orderDataService.SaveOrderDetails();

            //Assert
            _orderRepository.Verify(or => or.AddingOrder(_mockNewOrder), Times.Once());
            _customerRepository.Verify(cr => cr.UpdateCustomer(_mockNewOrder.Customer), Times.Never());
            _customerRepository.Verify(cr => cr.AddNewCustomer(_mockNewOrder.Customer), Times.Once());
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
