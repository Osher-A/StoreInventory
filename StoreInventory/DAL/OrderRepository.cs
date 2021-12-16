using Microsoft.EntityFrameworkCore;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyLibrary.Utilities;
using StoreInventory.Interfaces;

namespace StoreInventory.DAL
{
    public class OrderRepository : IOrderRepository
    {
        public List<Model.Order> GetOrders()
        {
            List<Order> orders;
            using (var db = new StoreContext())
            {
                orders = db.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrdersProducts)
                    .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category)
                    .OrderBy(o => o.OrderDate)
                    .ToList();
            }
            return orders;
        }

        public Order GetOrder(int Id)
        {
            var order = new Order();
            using (var db = new StoreContext())
            {
                order = db.Orders.Where(o => o.Id == Id)
                    .Include(o => o.Customer)
                    .Include(o => o.OrdersProducts)
                    .ThenInclude(op => op.Product)
                    .FirstOrDefault();
            }
            return order;
        }

        public void AddingOrder(IOrder newOrder)
        {
            Order newModelOrder = new Model.Order();
            MapToModelOrder(newOrder, newModelOrder);
            using (var db = new StoreContext())
            {
                db.Orders.Add(newModelOrder);
                db.SaveChanges();
            }
        }

        public void UpdateOrder(IOrder orderToEdit)
        {
            using (var db = new StoreContext())
            {
                Order modelOrder = db.Orders.Find(orderToEdit.Id);
                MapToModelOrder(orderToEdit, modelOrder);
                db.SaveChanges();
            }
        }

        public void DeletingOrder(int orderId)
        {
            using (var db = new StoreContext())
            {
                var orderToDelete = db.Orders.Find(orderId);
                db.Orders.Remove(orderToDelete);
                db.SaveChanges();
            }
        }

        public int GetCurrentOrderId()
        {
            using var db = new StoreContext();
            return db.Orders.OrderByDescending(o => o.OrderDate).Take(1).SingleOrDefault().Id;
        }

        public int GetOrderId(IOrder order)
        {
            using var db = new StoreContext();
            return db.Orders.SingleOrDefault(o => o.CustomerId == order.CustomerId
                                                    && o.OrderDate == order.OrderDate
                                                    && o.Total == order.Total).Id;
        }

        private void MapToModelOrder(IOrder order, Model.Order modelOrder)
        {
            if (order.CustomerId != 0)
                modelOrder.CustomerId = order.CustomerId;
            else
                modelOrder.Customer = (Model.Customer)(DTO.Customer)order.Customer; // Creating new model customer

            modelOrder.OrderDate = order.OrderDate;
            // modelOrder.Total = order.Total;  // This is set via the db trigger.
            modelOrder.AmountPaid = order.AmountPaid;

            // need to update products
        }
    }
}   


