using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.DTO
{
    public class Stock : IMapper
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }
    }
}
