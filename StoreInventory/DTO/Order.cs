using MyLibrary.Utilities;
using StoreInventory.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreInventory.DTO
{
    public class Order : IOrder, INotifyPropertyChanged
    {

        public int Id { get; set; }
        public ICustomer Customer
        {
            get { return _customer; }
            set 
            { 
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        private float _total;
        private float _amountPaid;
        private ICustomer _customer;

        public float Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
        public float AmountPaid
        {
            get { return _amountPaid; }
            set
            {
                _amountPaid = value;
                OnPropertyChanged(nameof(AmountPaid));
            }
        }
        public List<OrderProduct> OrdersProducts { get; set; }

        public Order()
        {
            OrdersProducts = new List<OrderProduct>();
        }

        public static explicit operator Order(Model.Order order)
        {
            return new Order
            {
                Id = order.Id,
                Customer = (Customer)(Model.Customer)order.Customer,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Total = order.Total,
                AmountPaid = order.AmountPaid
            };
        }

        public static explicit operator Model.Order(Order order)
        {
            return new Model.Order
            {
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Total = order.Total,
                AmountPaid = order.AmountPaid
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
