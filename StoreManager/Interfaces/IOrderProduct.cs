namespace StoreManager.Interfaces
{
    public interface IOrderProduct
    {
        IOrder Order { get; set; }
        int OrderId { get; set; }
        IProduct Product { get; set; }
        int ProductId { get; set; }
        int Quantity { get; set; }
    }
}