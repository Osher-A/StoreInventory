using MyLibrary.Extentions;
using MyLibrary.Utilities;
using StoreManager.DAL;
using StoreManager.DTO;
using StoreManager.Enums;
using StoreManager.Interfaces;
using StoreManager.Services.MessageService;
using StoreManager.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace StoreManager.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private ShoppingBasketService _shoppingBasketService = new ShoppingBasketService(new StockRepository());
        private OrderViewService _orderViewService;
        private PaymentService _paymentService;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IOrderProductRepository _orderProductRepository;

        private Stock _lastSelectedProduct = new Stock() { Product = new Product() };
        private Stock _selectedStockProduct = new Stock() { Product = new Product() };
        private BasketItem _selectedBasketItem;
        private Category _selectedCategory = new Category();
        private string _search;
        private int _productQuantity;
        private PaymentAmounts _paymentAmounts;

        private ObservableCollection<Stock> searchProductsStocked;
        private ObservableCollection<BasketItem> basketProducts = new ObservableCollection<BasketItem>();
        private PaymentStatus? _paymentStatus;
        private bool _isExpanded;
        private Order _newOrder = new Order { OrderDate = DateTime.Now, Customer = new Customer() { Address = new Address() } };

        public OrderViewModel(IMessageService messageService, IOrderRepository orderRepository,
            ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository)
        {
            _orderViewService = new OrderViewService(_shoppingBasketService);
            _paymentService = new PaymentService(messageService, NewOrder.Customer);
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderProductRepository = orderProductRepository;
            SearchCommand = new CustomCommand(SearchAllProducts, CanSearch);
            AddToBasketCommand = new CustomCommand(AddToBasket, CanAddToBasket);
            RemoveItemCommand = new CustomCommand(RemoveItem, CanRemoveItem);
            EmptyBasketCommand = new CustomCommand(EmptyBasket, CanEmptyBasket);
            PaymentCommand = new CustomCommand(ManagePayment, CanManagePayment);
            CheckOutCommand = new CustomCommand(CheckOut, CanCheckOut);
            BallanceCommand = new CustomCommand(ControllBalance, CanControllBalance);
            PaymentAmounts = new PaymentAmounts() { AmountOwed = NewOrder.Total };
            LoadData();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Order NewOrder
        {
            get { return _newOrder; }
            private set
            { 
                _newOrder = value;
                OnPropertyChanged(nameof(NewOrder));
            }
        }
        public Stock SelectedStockProduct 
        { 
            get { return _selectedStockProduct; }
            set
            {
                _selectedStockProduct = value;
                OnPropertyChanged(nameof(SelectedStockProduct));
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public BasketItem SelectedBasketItem
        {
            get { return _selectedBasketItem; }
            set
            { 
                _selectedBasketItem = value;
                OnPropertyChanged(nameof(SelectedBasketItem));
            }
        }
        public string Search
        {
            get { return _search; }
            set
            { 
                _search = value;
                OnPropertyChanged(nameof(Search));
            }
        }
        public int ProductQuantity 
        {
            get { return _productQuantity; }
            set {
                _productQuantity = value;
                OnPropertyChanged(nameof(ProductQuantity));
            }
        }

        public PaymentStatus? PaymentStatus
        {
            get { return _paymentStatus; }
            set
            {
                if (value == null)  
                    return;         // This way it'll only be updated via the radio button that has been selected (via the converter).
                _paymentStatus = value;
                OnPropertyChanged(nameof(PaymentStatus));
            }
        }

        public PaymentAmounts PaymentAmounts 
        {
            get { return _paymentAmounts; }
            set
            { 
                _paymentAmounts = value;
                OnPropertyChanged(nameof(PaymentAmounts));
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }
        public ObservableCollection<string> Categories { get; private set; }
        public ObservableCollection<Stock> SearchProductsStocked
        {
            get { return searchProductsStocked; }
            set 
            { 
                searchProductsStocked = value;
                OnPropertyChanged(nameof(SearchProductsStocked));
            }
        }
        public ObservableCollection<BasketItem> BasketProducts
        {
            get { return basketProducts; }
            set {
                basketProducts = value;
                OnPropertyChanged(nameof(BasketProducts));
            }
        }


        public ICommand SearchCommand { get; set; }
        public ICommand AddToBasketCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand EmptyBasketCommand { get; set; }
        public ICommand PaymentCommand { get; set; }
        public ICommand CheckOutCommand { get; set; }
        public ICommand BallanceCommand { get; set; }

        public event EventHandler ScrollUpEvent;
        private bool CanSearch(object obj)
        {
            return SelectedCategory != null;
        }
        private void SearchAllProducts(object obj)
        {
            SelectedCategory.Id = _orderViewService.GetCategoryId(SelectedCategory.Name);
            SearchProductsStocked = _orderViewService.GetSearchProducts(Search, SelectedCategory.Id);
        }
        private bool CanAddToBasket(object obj)
        {
            return SelectedStockProduct != null;
        }
        private void AddToBasket(object obj)
        {
             _shoppingBasketService.AddToOrder(SelectedStockProduct.Product, ProductQuantity);
            UpdateBasket(true);

            // Once the SearchAllProducts method is called the SearchProductsStocked list changes and the selectedStockProduct = null, which makes it impossible to change the qty through the text box. The solution is to save the selected product before SearchAllProducts is called.

            _lastSelectedProduct = SelectedStockProduct; 
            SearchAllProducts(null);
            ProductQuantity = 0;
            SelectedStockProduct = _lastSelectedProduct;   
        }
        private bool CanRemoveItem(object obj)
        {
            return SelectedBasketItem != null;
        }

        private void RemoveItem(object obj)
        {
            _shoppingBasketService.RemoveItemFromBasket(SelectedBasketItem.Product);
            UpdateBasket(false);
        }

        private bool CanEmptyBasket(object obj)
        {
            return true;
        }

        private void EmptyBasket(object obj)
        {
            _shoppingBasketService.EmptyBasket();
            UpdateBasket(false);
        }

        private void UpdateBasket(bool addingItem)
        {
            BasketProducts = _shoppingBasketService.BasketItems;
            NewOrder.Total = _shoppingBasketService.TotalCost;
            SearchAllProducts(null);

            // The following is needed when updating after payment-status radio button has been checked.
            ControllBalance(null);
            PaymentStatus = _paymentService.UpdatePaymentStatus(addingItem, PaymentStatus);
        }

        private bool CanControllBalance(object obj)
        {
            return PaymentAmounts.AmountPaid > 0;
        }

        private void ControllBalance(object obj)
        {
           PaymentAmounts.AmountOwed = _paymentService.OutStandingBallance(PaymentAmounts.AmountPaid, NewOrder.Total);
            ValidateDetails();
        }
        private bool CanManagePayment(object obj)
        {
            return NewOrder.Total > 0;
        }

        private void ManagePayment(object obj)
        {
             PaymentAmounts = _paymentService.PaymentController(PaymentStatus, NewOrder.Total);
            ValidateDetails();
        }
        private bool CanCheckOut(object obj)
        {
            return PaymentStatus != null;
        }

        private void CheckOut(object obj)
        {
            if (ValidateDetails())
            {
                NewOrder.AmountPaid = PaymentAmounts.AmountPaid;
                var odService = new OrderDataService(_orderRepository, _customerRepository, _orderProductRepository ,NewOrder, BasketProducts.ToList());
                odService.SaveOrderDetails();

                ClearAllInputs();
            }
        }

        private bool ValidateDetails()
        {
             _paymentService.CustomersDetailsValidated(PaymentStatus);
            if (!_paymentService.IsDetailsValid)
            { 
                ScrollUpEvent?.Invoke(this, new EventArgs());
                IsExpanded = true;
                return false;
            }

            IsExpanded = false;
            return true;
        }

        private void ClearAllInputs()
        {
            NewOrder =  new Order { OrderDate = DateTime.Now , Customer = new Customer() { Address = new Address() } };
            LoadData();
            EmptyBasket(null);
            PaymentAmounts = new PaymentAmounts() { AmountOwed = NewOrder.Total };
            PaymentStatus = Enums.PaymentStatus.NotPaid;
        }
        private void LoadData() 
        {
            var categories = _orderViewService.GetCategories();
            Categories = GetCategoriesNames(categories);
            SelectedCategory.Name = Categories[0];
            SearchProductsStocked = _orderViewService.AllProductsStocked;
           
        }
        private ObservableCollection<string> GetCategoriesNames(IEnumerable<Category> categories) 
        {
            var categoriesList = new ObservableCollection<string>();
            foreach (var category in categories)
                categoriesList.Add(category.Name);

            return categoriesList;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
