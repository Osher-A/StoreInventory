using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary.Utilities;

namespace StoreInventory.Model
{
    public class Stock :IMapper
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int QuantityInStock { get; set; }


    }
}
