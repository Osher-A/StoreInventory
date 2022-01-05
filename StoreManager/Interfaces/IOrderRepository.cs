using StoreManager.Interfaces;
using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface IOrderRepository
    {
        void AddingOrder(IOrder newOrder);
        void DeletingOrder(int orderId);
        int GetCurrentOrderId();
        Model.Order GetOrder(int Id);
        int GetOrderId(IOrder order);
        List<Model.Order> GetOrders();
        void UpdateOrderStatus(IOrder orderToEdit);
    }
}