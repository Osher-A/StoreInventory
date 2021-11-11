using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StoreInventory.Services.OrderServices
{
    public class ShoppingBasketService 
    {
        public ObservableCollection<BasketItem> BasketItems = new ObservableCollection<BasketItem>();
        private IStockRepository _stockRepository;
        private List<IStock> _modelFullStock;

        public List<IStock> StockRemaing { get; private set; }
        private float _totalCost;

        public float TotalCost => _totalCost = BasketItems.Count >= 1 ? BasketItems[BasketItems.Count - 1].RunningTotal : 0f;


        public ShoppingBasketService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _modelFullStock = stockRepository.GetAllStocks();
            PreOrderStock();
        }

        public void AddToOrder(IProduct product, int quantity)
        {
            AddToBasket(product, quantity);

            float runningTotal = 0;
            foreach (var item in BasketItems)
            {
                runningTotal += item.Product.Price * item.Quantity;
                item.RunningTotal = runningTotal;
            }
        }

        public void RemoveItemFromBasket(IProduct product)
        {
            var itemToRemove = BasketItems.Single(bi => bi.Product.Id == product.Id);
            BasketItems.Remove(itemToRemove);
            UpdateStock();
        }

        public void EmptyBasket()
        {
            BasketItems = new ObservableCollection<BasketItem>();
            UpdateStock();
        }

        private void AddToBasket(IProduct product, int quantity)
        {
            var basketItem = new BasketItem() { Product = product, Quantity = quantity == 0 ? 1 : quantity };

            var existingItem = BasketItems.SingleOrDefault(bi => bi.Product.Id == basketItem.Product.Id);

            if (existingItem != null)
                existingItem.Quantity = quantity == 0 ? existingItem.Quantity + 1 : quantity;
            else
                BasketItems.Add(basketItem);

            UpdateStock();
        }

        private void UpdateStock()
        {
            if (BasketItems.Count < 1)
                PreOrderStock();
            else
            {
                foreach (var item in BasketItems)
                {
                    IStock uiProduct = StockRemaing.Single(ws => ws.Product.Id == item.Product.Id);
                    IStock modelProduct = _modelFullStock.Single(ms => ms.ProductId == item.Product.Id);
                    uiProduct.QuantityInStock = modelProduct.QuantityInStock - item.Quantity;
                }
            }
        }

        private void PreOrderStock()
        {
            StockRemaing = _stockRepository.GetAllStocks().OrderBy(s => s.Product.Category.Name).ThenBy(s => s.Product.Name).ToList();
        }

    }
}
