using MyLibrary.Utilities;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StoreInventory.Model
{
    public class Order : IOrder
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; } 
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public float Total { get; set; }
        public float AmountPaid { get; set; }
        public IEnumerable<OrderProduct> OrdersProducts { get; set; }
        [NotMapped]
        IEnumerable<IOrderProduct> IOrder.OrdersProducts
        { 
            get { return OrdersProducts; } set { OrdersProducts = value as IEnumerable<OrderProduct>; } 
        }

        public Order()
        {
            OrdersProducts = new List<OrderProduct>();
        }
    }
}
