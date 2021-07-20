using MyLibrary;
using MyLibrary.Utilities;
using StoreInventory.DAL;
using StoreInventory.DAL.Interfaces;
using StoreInventory.DTO;
using StoreInventory.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace StoreInventory.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged
    {
        private IProductRepository _productRepository = new ProductRepository();
        private StockService _stockService = new StockService();
        private ProductService _productService;
        private ImageService _imageService = new ImageService();

        private DTO.Stock _selectedStock = new Stock() { Product = new Product() };
        public DTO.Stock SelectedStock
        {
            get { return _selectedStock; }
            set
            {
                _selectedStock = value;
                OnPropertyChanged(nameof(SelectedStock));
            }
        }
        public BitmapImage SelectedImage { get; private set; } 
        public ICommand NewProductToStockCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }
        public ICommand ImageCommand { get; set; }
        public ObservableCollection<Stock> GetStocks { get; set; } = new ObservableCollection<Stock>();
        public ObservableCollection<Category> Categories { get; set; }
        
        public StockViewModel()
        {
            _productService = new ProductService(_productRepository);
            SelectedImage = (SelectedStock.Product.Image != null) ? _imageService.ByteArrayToBitmapImage(SelectedStock.Product.Image) : new BitmapImage();
            LoadData();
            NewProductToStockCommand = new CustomCommand(AddNewProduct, CanAddNewProduct);
            ImageCommand = new CustomCommand(AddImage, CanAddImage);
            UpdateProductCommand = new CustomCommand(UpdateProduct, CanUpDateProduct);
        }

        private void UpdateProduct(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanUpDateProduct(object obj)
        {
            return true;
        }

        private void AddImage(object obj)
        {
            var usersImage = _imageService.GetUsersImage();
            SelectedStock.Product.Image = usersImage;
        }

        private bool CanAddImage(object obj)
        {
            return true;
        }

        private void AddNewProduct(object obj)
        {
            if (_productService.ValidProductToAdd(SelectedStock))
            {
                _productRepository.AddingProduct(SelectedStock.Product);
                LoadData();
            }
        }

        private bool CanAddNewProduct(object obj)
        {
            return !_productService.ExistingProduct(SelectedStock.Product);
        }

        private void LoadData()
        {
            GetStocks = _stockService.GetStocks();
            Categories = _stockService.GetCategories();
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
