using StoreManager.Model;
using System;
using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface IOrder
    {
        float AmountPaid { get; set; }
        ICustomer Customer { get; set; }
        int? CustomerId { get; set; }
        int Id { get; set; }
        DateTime OrderDate { get; set; }
        float Total { get; set; }
        IEnumerable<IOrderProduct> OrdersProducts { get; set; }
    }
}