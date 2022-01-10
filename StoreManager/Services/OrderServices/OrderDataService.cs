using StoreManager.DAL;
using StoreManager.DTO;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StoreManager.Services.OrderServices
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
            SetExistingCustomerField();
            CreateOrUpdateExistingCustomer();

            _orderRepository.AddingOrder((DTO.Order)_newOrder);

            RemoveProductsFromDb();
        }
        private void SetExistingCustomerField()
        {
            _existingCustomer = (ICustomer)_customerRepository.GetCustomers().FirstOrDefault
                (c => !string.IsNullOrWhiteSpace(c.Email) && c.Email?.Trim() == _newOrder.Customer.Email?.Trim()
            || (!string.IsNullOrWhiteSpace(c.Address?.Zip) && c.Address?.Zip == _newOrder.Customer?.Address?.Zip
            && !string.IsNullOrWhiteSpace(c.Address?.House) && c.Address?.House?.Trim().ToLower() == _newOrder.Customer?.Address?.House?.Trim().ToLower())
            || (!string.IsNullOrWhiteSpace(c.Address?.House) && c.Address?.House?.Trim().ToLower() == _newOrder.Customer?.Address?.House?.Trim().ToLower()
            && !string.IsNullOrWhiteSpace(c.Address?.Street) && c.Address?.Street?.Trim().ToLower() == _newOrder.Customer?.Address?.Street));
        }

        private void CreateOrUpdateExistingCustomer()
        {
            if( _existingCustomer != null )
            {
                GetExistingCustomerIdAndAddressId(); 
                _customerRepository.UpdateCustomer(_newOrder.Customer);
            }
            else if(IsUniqueDetails())
            {
                _customerRepository.AddNewCustomer(_newOrder.Customer);
                _newOrder.CustomerId = _customerRepository.GetLastCustomerId();
            }
        }
        private void GetExistingCustomerIdAndAddressId()
        {
            _newOrder.Customer.Id = _existingCustomer.Id;
            if (_newOrder.Customer?.Address != null && _existingCustomer.Address != null)
                    _newOrder.Customer.Address.Id = _existingCustomer.Address.Id;
        }
        private bool IsUniqueDetails()
        {
            if (_newOrder.Customer != null)
                if (_newOrder.Customer.Address != null)
                    if ((String.IsNullOrWhiteSpace(_newOrder.Customer.Address.House) ||
                        (string.IsNullOrWhiteSpace(_newOrder.Customer.Address.Street)
                        && String.IsNullOrWhiteSpace(_newOrder.Customer.Address.Zip)))
                        && string.IsNullOrWhiteSpace(_newOrder.Customer.Email))
                        return false;
                    else
                        return true;
                else if (!string.IsNullOrWhiteSpace(_newOrder.Customer.Email))
                    return true;
                
            return false;
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
