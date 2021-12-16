using StoreInventory.DAL;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreInventory.Services.OrderControllerServices
{
    internal class OrderProductsService
    {
        private OrderRepository _orderRepository = new OrderRepository();
        
        public DTO.Order GetOrderWithProducts(IOrder order)
        {
            return ConvertToDTOOrder(_orderRepository.GetOrder(order.Id));
        }

        private DTO.Order ConvertToDTOOrder(Model.Order order)
        {   // This can not be done in the DTO.Order class converting with operator
            // that would result in a stack overflow exception
            return new DTO.Order
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrdersProducts = GetOrderProducts(order.OrdersProducts).ToList(),
                AmountOwed = ((DTO.Order)order).AmountOwed,
                AmountPaid = ((DTO.Order)order).AmountPaid,
                Total = order.Total,
                Customer = order.Customer
            };
        }

        private IEnumerable<DTO.OrderProduct> GetOrderProducts(List<Model.OrderProduct> modelOrderProducts)
        {
            foreach(var orderProduct in modelOrderProducts)
            {
                yield return new DTO.OrderProduct
                {
                    Order = orderProduct.Order,
                    Product = orderProduct.Product,
                    Quantity = orderProduct.Quantity,
                };
            };

            
        }
    }
}
