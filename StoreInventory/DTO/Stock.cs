using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.DTO
{
    public class Stock 
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int QuantityInStock { get; set; }

        public static explicit operator Stock(Model.Stock modelStock)
        {
            var dtoStock = new Stock();
            dtoStock.Id = modelStock.Id;
            dtoStock.Product = (DTO.Product)modelStock.Product;
            dtoStock.QuantityInStock = modelStock.QuantityInStock;

            return dtoStock;
        }
    }
}
