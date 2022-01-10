using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary.Utilities;
using StoreManager.Enums;
using StoreManager.Interfaces;

namespace StoreManager.Model
{
    public class Stock : IStock
    {
        public int Id { get; set; }
        public IProduct Product { get; set; } 
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
        public StockStatus StockStatus { get; set; }
    }
}
