using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface IOrderProductRepository
    {
        void UpdateOrderProducts(List<IOrderProduct> orderProducts);
    }
}