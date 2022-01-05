using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.DTO
{
    public class StockIn
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateIn { get; set; }
        public float CostPrice { get; set; }
    }
}
