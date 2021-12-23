using StoreInventory.DAL;
using StoreInventory.DTO;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StoreInventory.Services.OrderServices
{
    public class OrderDataService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IOrder _newOrder;
        private ICustomer _existingCustomer;
        private readonly List<BasketItem> _basketItems;

        public OrderDataService(IOrderRepository orderRepo, ICustomerRepository customerRepo,
            IOrderProductRepository orderProductRepository, IOrder newOrder, List<BasketItem> basketItems)
        {
            _orderRepository = orderRepo;
            _customerRepository = customerRepo;
            _orderProductRepository = orderProductRepository;
            _newOrder = newOrder;
            _basketItems = basketItems;
        }

        public void SaveOrderDetails()
        {
            var customerid = SetAndOrGetCustomersId();
            if (customerid != 0)
                _newOrder.CustomerId = customerid;

            _orderRepository.AddingOrder((DTO.Order)_newOrder);
            RemoveProductsFromDb();
            FetchCustomer();
        }

        private int SetAndOrGetCustomersId()
        {
            if (_existingCustomer != null)
            {
                _customerRepository.UpdateCustomer(_newOrder.Customer);
                return _existingCustomer.Id;
            }
            else if (_existingCustomer == null && IsUniqueDetails())
            {
                _customerRepository.AddNewCustomer(_newOrder.Customer);
                return _customerRepository.GetLastCustomerId();
            }
            return 0;
        }
        
        private bool IsUniqueDetails()
        {
            if (_newOrder.Customer != null)
                if (_newOrder.Customer.Address != null)
                    if (String.IsNullOrWhiteSpace(_newOrder.Customer.Address.House)||
                        (string.IsNullOrWhiteSpace(_newOrder.Customer.Address.Street)
                        && String.IsNullOrWhiteSpace(_newOrder.Customer.Address.Zip))
                        && string.IsNullOrWhiteSpace(_newOrder.Customer.Email))
                        return false;
                    else
                        return true;
                
            return false;
        }

        private void FetchCustomer()
        {
            _existingCustomer = (ICustomer)_customerRepository.GetCustomers().FirstOrDefault(c => c.Email.Trim() == _newOrder.Customer.Email.Trim()
            || (c.Address.Zip == _newOrder.Customer.Address.Zip && c.Address.House.Trim().ToLower() == _newOrder.Customer.Address.House.Trim().ToLower())
            || (c.Address.House.Trim().ToLower() == _newOrder.Customer.Address.House.Trim().ToLower() && c.Address.Street.Trim().ToLower() == _newOrder.Customer.Address.Street));
        }

        private void RemoveProductsFromDb()
        {
            List<IOrderProduct> orderProducts = CreateOrderProductsList();

            _orderProductRepository.UpdateOrderProducts(orderProducts);
        }

        private List<IOrderProduct> CreateOrderProductsList()
        {
            var orderProducts = new List<IOrderProduct>();
            foreach (var item in _basketItems)
            {
                var orderProduct = new OrderProduct() { OrderId = GetOrderId(), ProductId = item.Product.Id, Quantity = item.Quantity };
                orderProducts.Add(orderProduct);
            }

            return orderProducts;
        }

        private int GetOrderId()
        {
            return _orderRepository.GetCurrentOrderId();
        }
    }
}
