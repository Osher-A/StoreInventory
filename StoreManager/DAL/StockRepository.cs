using Microsoft.EntityFrameworkCore;
using StoreManager.Interfaces;
using StoreManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManager.DAL
{
    public class StockRepository : IStockRepository
    {
        //No need to add a AddToStock Method since this is automated by the db Triggers of the ordersProducts and stockIn tables

        public List<IStock> GetAllStocks()
        {
            using (var db = new StoreContext())
            {
                return db.Stocks.Include(s => s.Product).ThenInclude(p => p.Category).ToList<IStock>();
            }
        }
       
        public IEnumerable<IStock> SearchStocks(List<IProduct> searchProducts)
        {
            var allStocks = GetAllStocks(); 
            foreach (var product in searchProducts)
                yield return allStocks.SingleOrDefault(s => s.ProductId == product.Id);
        }
    }
}
