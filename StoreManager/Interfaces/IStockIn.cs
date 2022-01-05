using System;

namespace StoreManager.Interfaces
{
    public interface IStockIn
    {
        float CostPrice { get; set; }
        DateTime DateIn { get; set; }
        int Id { get; set; }
        IProduct Product { get; set; }
        int ProductId { get; set; }
        int Quantity { get; set; }
    }
}