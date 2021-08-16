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
    class OrderRepository
    {
        public List<Order> GetOrders()
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
                order = db.Orders.Find(Id);
            }
            return order;
        }

        //public void AddingOrder(IOrder newOrder)
        //{
        //    Order newModelOrder = MyMapper.Mapper(newOrder, new Order());
        //    using(var db = new StoreContext())
        //    {
        //        db.Orders.Add(newModelOrder);
        //        db.SaveChanges();
        //    }
        //}

        //public void EditingOrder(DTO.Order orderToEdit)
        //{
        //    using(var db = new StoreContext())
        //    {
        //        Order modelOrder =  db.Orders.Find(orderToEdit.Id);
        //        MyMapper.Mapper(orderToEdit, modelOrder);
        //        db.SaveChanges();
        //    }
        //}

        public void DeletingOrder(int orderId)
        {
            using(var db = new StoreContext())
            {
                var orderToDelete = db.Orders.Find(orderId);
                db.Orders.Remove(orderToDelete);
                db.SaveChanges();
            }
        }


    }
}   


