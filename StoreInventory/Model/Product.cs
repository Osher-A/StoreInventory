﻿using MyLibrary.Utilities;
using StoreInventory.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Model
{
    public class Product : IMapper
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public UnitType UnitType { get; set; }
        public byte[] Image { get; set; }
        public Stock Stock { get; set; }
        public List<StockIn> StockIns { get; set; }
        public List<OrderProduct> OrdersProducts { get; set; }
        public Product()
        {
            StockIns = new List<StockIn>();
            OrdersProducts = new List<OrderProduct>();
        }
    }
}
