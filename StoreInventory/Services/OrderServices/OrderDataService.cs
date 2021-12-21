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
        private CustomerRepository _customerRepository = new CustomerRepository();
        private OrderRepository _orderRepository = new OrderRepository();
        private readonly IOrder _newOrder;
        private readonly List<BasketItem> _basketItems;

        public OrderDataService(IOrder newOrder, List<BasketItem> basketItems)
        {
            _newOrder = newOrder;
            _basketItems = basketItems;
        }

        public void SaveOrderDetails()
        {
            _newOrder.CustomerId = SetAndOrGetCustomersId();
            _orderRepository.AddingOrder((DTO.Order)_newOrder);
            RemoveProductsFromDb();
        }

        private int SetAndOrGetCustomersId()
        {
            ICustomer existingCustomer = ExistingCustomer();
            if (existingCustomer != null)
            {
                _customerRepository.UpdateCustomer(_newOrder.Customer);
                return existingCustomer.Id;
            }
            else
            {
                _customerRepository.AddNewCustomer(_newOrder.Customer);
                return _customerRepository.GetLastCustomerId();
            }
        }
        
        private ICustomer ExistingCustomer()
        {
            ICustomer customer = FetchCustomer();
            if (customer != null)
                if (customer.Address != null)
                    if (String.IsNullOrWhiteSpace(customer.Address.House)|| (string.IsNullOrWhiteSpace(customer.Address.Street)
                        && String.IsNullOrWhiteSpace(customer.Address.Zip)) && string.IsNullOrWhiteSpace(customer.Email))
                        return null;
                    else
                        return customer;
                else if (!string.IsNullOrWhiteSpace(customer.Email))
                    return customer;
            return null;
        }

        private ICustomer FetchCustomer()
        {
            return (ICustomer)_customerRepository.GetCustomers().FirstOrDefault(c =>
            c.FirstNames?.Trim().ToLower() == _newOrder.Customer?.FirstNames?.Trim().ToLower()
                    && c.LastName?.Trim().ToLower() == _newOrder.Customer?.LastName?.Trim().ToLower());
        }

        private void RemoveProductsFromDb()
        {
            List<IOrderProduct> orderProducts = CreateOrderProductsList();

            var orderProductRepo = new OrderProductRepository();
            orderProductRepo.UpdateOrderProducts(orderProducts);
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
