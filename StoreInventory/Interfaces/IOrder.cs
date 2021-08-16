using System;
using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface IOrder
    {
        float AmountPaid { get; set; }
        ICustomer Customer { get; set; }
        int CustomerId { get; set; }
        int Id { get; set; }
        DateTime OrderDate { get; set; }
        float TotalPrice { get; set; }
    }
}