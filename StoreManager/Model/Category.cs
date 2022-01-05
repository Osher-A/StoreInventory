using MyLibrary.Utilities;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Model
{
    public class Category : ICategory
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
