using MyLibrary.Utilities;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StoreManager.DTO
{
    public class Order : IOrder, INotifyPropertyChanged
    {
        private float _total;
        private float _amountPaid;
        private float _amountOwed;
        private ICustomer _customer;

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
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
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

        public float AmountOwed
        {
            get {return Total - AmountPaid;}    
            set 
            {
                _amountOwed = value;
                AmountPaid = Total - value;
                OnPropertyChanged(nameof(AmountOwed));
            }
        }
        public IEnumerable<IOrderProduct> OrdersProducts { get; set; }

        public Order()
        {
            OrdersProducts = new List<IOrderProduct>();
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
                AmountPaid = order.AmountPaid,
                AmountOwed = order.Total - order.AmountPaid,
                OrdersProducts = GetIOrderProducts(order.OrdersProducts)
            };
        }

        public static explicit operator Model.Order(Order order)
        {
            return new Model.Order
            {
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                //Total = order.Total, -- Set via db trigger
                AmountPaid = order.AmountPaid,
                OrdersProducts = GetIOrderProducts(order.OrdersProducts) as IEnumerable<Model.OrderProduct>
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static IEnumerable<IOrderProduct> GetIOrderProducts(IEnumerable<IOrderProduct> orderProducts)
        {
            IOrderProduct orderProduct;
            if (orderProducts is IEnumerable<Model.OrderProduct>)
                orderProduct = new DTO.OrderProduct();
            else
                orderProduct = new Model.OrderProduct();
            return CreateOrderProducts(orderProducts, orderProduct);
        }

        private static IEnumerable<IOrderProduct> CreateOrderProducts(IEnumerable<IOrderProduct> orderProducts, IOrderProduct orderProduct)
        {
            var iOrderProducts = new List<IOrderProduct>();
            foreach (var modelOrderProduct in orderProducts)
            {
                orderProduct.OrderId = modelOrderProduct.OrderId;
                orderProduct.ProductId = modelOrderProduct.ProductId;
                orderProduct.Product = modelOrderProduct.Product;
                orderProduct.Quantity = modelOrderProduct.Quantity;
                iOrderProducts.Add(orderProduct);
            }
            return iOrderProducts;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
