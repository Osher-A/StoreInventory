using StoreManager.Enums;

namespace StoreManager.Interfaces
{
    public interface IStock
    {
        int Id { get; set; }
        IProduct Product { get; set; }
        int ProductId { get; set; }
        int QuantityInStock { get; set; }
        StockStatus StockStatus { get; set; }
    }
}