using MyLibrary.Utilities;
using StoreInventory.Enums;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreInventory.DTO
{
    public class Stock : IStock, INotifyPropertyChanged
    {

        public int Id { get; set; }
        public IProduct Product { get; set; }    
        public int ProductId { get; set; }

        private int _quantityInStock;
        private StockStatus stockStatus;


        public int QuantityInStock
        {
            get {return _quantityInStock; }
            set
            {
               _quantityInStock = value;
                OnPropertyChanged(nameof(QuantityInStock));
                SetStockStatus();
            }
    }
        public StockStatus StockStatus
        {
            get { return stockStatus; }
            set 
            {
                stockStatus = value;
                OnPropertyChanged(nameof(StockStatus));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public static explicit operator Stock(Model.Stock modelStock)
        {
            var dtoStock = new Stock();
            dtoStock.Id = modelStock.Id;
            dtoStock.Product = (DTO.Product)(Model.Product)modelStock.Product;
            dtoStock.Product.Category = (DTO.Category)(Model.Category)modelStock.Product.Category;
            dtoStock.QuantityInStock = modelStock.QuantityInStock;
            return dtoStock;
        }

        private void SetStockStatus()
        {
            int wellStocked;

            switch (this.Product.Category.Name)
            {
                case "Home":
                    wellStocked = 15;
                    break;      
                case "Clothes":
                    wellStocked = 20;
                    break;
                default:
                    wellStocked = 25;
                    break;
            }
            GetStatus(wellStocked);
        }
        private void GetStatus(int wellStocked)
        {
            if (this.QuantityInStock <= 0)
                this.StockStatus = StockStatus.OutOfStock;
            else if (this.QuantityInStock >= wellStocked)
                this.StockStatus = StockStatus.WellStocked;
            else
                this.StockStatus = StockStatus.LowInStock;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
