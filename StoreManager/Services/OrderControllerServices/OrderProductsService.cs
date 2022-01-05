using StoreManager.DAL;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManager.Services.OrderControllerServices
{
    internal class OrderProductsService
    {
        private OrderRepository _orderRepository = new OrderRepository();
        
        public DTO.Order GetOrderWithProducts(IOrder order)
        {
           return (DTO.Order)(Model.Order)_orderRepository.GetOrder(order.Id);
        }
    }
}
