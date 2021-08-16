using StoreInventory.Model;
using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface IStockRepository
    {
        List<IStock> GetAllStocks();
        IEnumerable<IStock> SearchStocks(List<IProduct> searchProducts);
    }
}
