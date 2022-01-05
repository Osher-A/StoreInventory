using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.DTO
{
    public class OrderProduct : IOrderProduct
    {
        public IOrder Order { get; set; }
        public int OrderId { get; set; }
        public IProduct Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        
    }
}
