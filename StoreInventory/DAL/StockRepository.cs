using Microsoft.EntityFrameworkCore;
using StoreInventory.DAL.Interfaces;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreInventory.DAL
{
    public class StockRepository : IStockRepository
    {
        //No need to add a AddToStock Method since this is automated by the db Triggers of the orders and stockOut tables

        public List<Stock> GetStocks()
        {
            var stocks = new List<Stock>();
            using (var db = new StoreContext())
            {
                stocks = db.Stocks.Include(s => s.Product).ThenInclude(p => p.Category).ToList();
            }

            //// This is needed so the Mapper in StockService should not throw an exception
            //foreach (var stock in stocks)
            //    stock.Product.Stock = null;
            return stocks;
        }
    }
}
