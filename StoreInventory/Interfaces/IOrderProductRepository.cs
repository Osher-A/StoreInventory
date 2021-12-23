using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface IOrderProductRepository
    {
        void UpdateOrderProducts(List<IOrderProduct> orderProducts);
    }
}