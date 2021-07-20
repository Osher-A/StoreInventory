using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Model
{
    public class Category : IMapper
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
