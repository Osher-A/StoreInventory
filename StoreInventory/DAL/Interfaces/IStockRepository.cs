using StoreInventory.Model;
using System.Collections.Generic;

namespace StoreInventory.DAL.Interfaces
{
    public interface IStockRepository
    {
        List<Stock> GetStocks();
    }
}
