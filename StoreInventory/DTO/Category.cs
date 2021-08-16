using MyLibrary.Utilities;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.DTO
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public static explicit operator Category(Model.Category category)
        {
            return new Category { Id = category.Id, Name = category.Name };
        }
    }
}
