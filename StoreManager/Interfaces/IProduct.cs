using MyLibrary.Utilities;
using StoreManager.Interfaces;
using StoreManager.Enums;
using StoreManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Interfaces
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
