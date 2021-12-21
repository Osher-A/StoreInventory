using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Services.OrderControllerServices
{
    internal class OrderUpdateService
    {
        private IOrderRepository _orderRepository;
        public OrderUpdateService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void UpdateOrder(IOrder order)
        {
            _orderRepository.UpdateOrderStatus(order);
        }
    }
}
