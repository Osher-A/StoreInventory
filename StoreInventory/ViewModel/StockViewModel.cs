using MyLibrary;
using MyLibrary.Utilities;
using StoreInventory.DAL;
using StoreInventory.Interfaces;
using StoreInventory.DTO;
using StoreInventory.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;

namespace StoreInventory.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged
    {
        private IProductRepository _productRepository = new ProductRepository();
        private StockService _stockService = new StockService(new StockRepository(), new CategoryRepository());
        private ProductDataService _productService;
        private ImageService _imageService = new ImageService();


        private ObservableCollection<Stock> _getStocks;

        public ObservableCollection<Stock> GetStocks
        {
            get { return _getStocks; }
            private set
            {
                _getStocks = value;
                OnPropertyChanged(nameof(GetStocks));
            }
        }
        private ObservableCollection<Stock> _lowInStockProducts;

        public ObservableCollection<Stock> LowInStockProducts
        {
            get { return _lowInStockProducts; }
            private set
            {
                _lowInStockProducts = value;
                OnPropertyChanged(nameof(LowInStockProducts));
            }
        }

        private ObservableCollection<Stock> _outOfStockProducts;

        public ObservableCollection<Stock> OutOfStockProducts
        {
            get { return _outOfStockProducts; }
            private set
            {
                _outOfStockProducts = value;
                OnPropertyChanged(nameof(OutOfStockProducts));
            }
        }
        public ObservableCollection<Category> Categories { get; set; }

        private DTO.Stock _selectedStock = new Stock() { Product = new Product() { Category = new Category() } };
        public DTO.Stock SelectedStock
        {
            get
            {
                if (_selectedStock.Product.Image == null && !string.IsNullOrWhiteSpace(_selectedStock.Product.Name))
                    ImageUploadVisibility = Visibility.Visible;
                else
                    ImageUploadVisibility = Visibility.Collapsed;
                return _selectedStock;                                
            }
            set
            {
                _selectedStock = value;
                OnPropertyChanged(nameof(SelectedStock));
            }                                                                 
        }
        private string _searchInput;

        public string SearchInput
        {
            get { return _searchInput; }
            set
            { 
                _searchInput = value;
                OnPropertyChanged(nameof(SearchInput));
            }
        }

        private Visibility _imageUploadVisibility;
        public Visibility ImageUploadVisibility
        {
            get { return _imageUploadVisibility; }
            set
            {
                _imageUploadVisibility = value;
                OnPropertyChanged(nameof(ImageUploadVisibility));
            }
        }
        public ICommand ClearFormCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }
        public ICommand AddNewProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand SearchCommand { get; set; }
       
        
        public StockViewModel()
        {
            _getStocks = new ObservableCollection<Stock>();
            _productService = new ProductDataService(_productRepository);
            LoadData();
            ClearFormCommand = new CustomCommand(ClearForm, CanClearForm);
            ImageCommand = new CustomCommand(AddImage, CanAddImage);
            UpdateProductCommand = new CustomCommand(UpdateProduct, CanUpDateProduct);
            AddNewProductCommand = new CustomCommand(AddNewProduct, CanAddNewProduct);
            DeleteProductCommand = new CustomCommand(DeleteProduct, CanDeleteProduct);
            SearchCommand = new CustomCommand(SearchProducts, CanSearchProducts);
        }

        private bool CanSearchProducts(object obj)
        {
            return true;
        }

        private void SearchProducts(object obj)
        {
           GetStocks = _stockService.SearchStocks(SearchInput, _productRepository);
        }

        private void DeleteProduct(object obj)
        {
            _productService.DeleteProduct(SelectedStock.Product.Id);
        }

        private bool CanDeleteProduct(object obj)
        {
            if (SelectedStock.Product.Id != 0)
                return true;
            else
                return false;
        }

        private void AddNewProduct(object obj)
        {
            _productService.AddNewProduct(SelectedStock.Product);
            LoadData();
            SelectedStock = new Stock() { Product = new Product() };
        }

        private bool CanAddNewProduct(object obj)
        {
            return !_productService.ExistingProduct(SelectedStock.Product);
        }

        private void UpdateProduct(object obj)
        {
            _productService.UpdateProduct(SelectedStock.Product);
            LoadData();
            SelectedStock = new Stock() { Product = new Product() };
        }

        private bool CanUpDateProduct(object obj)
        {
            return true;
        }

        private void AddImage(object obj)
        {
           SelectedStock.Product.Image = _imageService.GetUsersImage();
        }

        private bool CanAddImage(object obj)
        {
            return true;
        }

        private void ClearForm(object obj)
        {
            SelectedStock = new Stock() { Product = new Product() };
        }

        private bool CanClearForm(object obj)
        {
            return true;
        }

        private void LoadData()
        {
            GetStocks = _stockService.GetStocks;
            LowInStockProducts = _stockService.GetLowInStockProducts();
            OutOfStockProducts = _stockService.GetOutOfStockProducts();
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
