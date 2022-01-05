using StoreManager.Model;
using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface IStockRepository
    {
        List<IStock> GetAllStocks();
        IEnumerable<IStock> SearchStocks(List<IProduct> searchProducts);
    }
}
