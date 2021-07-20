using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Model
{
    public class Order : IMapper
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public float AmountPaid { get; set; }
        public List<OrderProduct> OrdersProducts { get; set; }

        public Order()
        {
            OrdersProducts = new List<OrderProduct>();
        }
    }
}
