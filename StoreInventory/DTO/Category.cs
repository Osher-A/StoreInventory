using MyLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.DTO
{
    public class Category 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Category()
        {
            Products = new List<Product>();
        }

        public static explicit operator Category(Model.Category category)
        {
            return new Category { Id = category.Id, Name = category.Name };
        }
    }
}
