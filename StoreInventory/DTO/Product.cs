using MyLibrary.Utilities;
using StoreInventory.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreInventory.DTO 
{
    public class Product: IMapper, INotifyPropertyChanged
    {
        private int _id;

        public int  Id
        {
            get { return _id; }
            set
            { 
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private Category _category;

        public Category Category
        {
            get { return _category; }
            set
            { 
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public int CategoryId { get; set; }

        private float _price;
        public float Price
        {
            get { return _price; }
            set 
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private UnitType _unitType;

        public UnitType UnitType
        {
            get { return _unitType; }
            set 
            { 
                _unitType = value;
                OnPropertyChanged(nameof(UnitType));
            }
        }

        private byte[] _image;

        public byte[] Image
        {
            get { return _image; }
            set 
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        private Stock _stock;

        public Stock Stock
        {
            get { return _stock; }
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }

        public List<StockIn> StockIns { get; set; }
        public List<OrderProduct> OrdersProducts { get; set; }
        public Product()
        {
            StockIns = new List<StockIn>();
            OrdersProducts = new List<OrderProduct>();
        }

        public static explicit operator DTO.Product(Model.Product product)
        {
            DTO.Product dtoProduct = new DTO.Product();
            dtoProduct.Id = product.Id;
            dtoProduct.Category = new Category { Name = product.Category.Name };
            dtoProduct.Name = product.Name;
            dtoProduct.Description = product.Description;
            dtoProduct.Price = product.Price;

            return dtoProduct;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
