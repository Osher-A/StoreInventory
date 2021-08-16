using MyLibrary.Utilities;
using StoreInventory.Interfaces;
using StoreInventory.Enums;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Interfaces
{
    public interface IProduct
    {
        ICategory Category { get; set; }
        int CategoryId { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        byte[] Image { get; set; }
        string Name { get; set; }
        float Price { get; set; }
        IStock Stock { get; set; }
        UnitType UnitType { get; set; }
    }
}
