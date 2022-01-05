using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Model
{
    public class StockIn : IStockIn
    {
        public int Id { get; set; }
        public IProduct Product { get; set; } 
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateIn { get; set; }
        public float CostPrice { get; set; }
    }
}
