using StoreInventory.Interfaces;
using StoreInventory.Services.MessageService;
using StoreInventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreInventory.Views.Pages
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage(IMessageService messageService, IOrderRepository orderRepository,
            ICustomerRepository customerRepository, IOrderProductRepository orderProductRepository)
        {
            InitializeComponent();

            var vm = new OrderViewModel(messageService, orderRepository, customerRepository, orderProductRepository);
            this.DataContext = vm;

            vm.ScrollUpEvent += Vm_ScrollUpEvent;
        }

        public Action<string> Navigator { get; set; }
        public Action<string> PageNavigator { get; set; }

        private void Vm_ScrollUpEvent(object sender, EventArgs e)
        {
            this.scrollViewer.ScrollToTop();
        }

        private void NavigationClick(object sender, RoutedEventArgs e)
        {
            string direction = (sender as Button).CommandParameter.ToString();
            Navigator?.Invoke(direction);
        }
        public void PageClick(object sender, RoutedEventArgs e)
        {
            var page = (sender as Button).CommandParameter as string;
            PageNavigator?.Invoke(page);
        }
    }
}
