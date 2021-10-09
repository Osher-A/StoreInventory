using StoreInventory.DAL;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StoreInventory.Services.OrderServices
{

    public class BasketItem : INotifyPropertyChanged
    {
        private int quantity;
        private float runningTotal;

        public IProduct Product { get; set; }
        public int Quantity
        {
            get { return quantity; }
            set
            { 
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public float RunningTotal
        {
            get { return runningTotal; }
            set
            {
                runningTotal = value;
                OnPropertyChanged(nameof(RunningTotal));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
