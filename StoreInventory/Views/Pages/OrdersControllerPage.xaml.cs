using StoreInventory.Interfaces;
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
    /// Interaction logic for OrdersControllerPage.xaml
    /// </summary>
    public partial class OrdersControllerPage : Page
    {
        public OrdersControllerPage(IEmailSmtpService emailSmtpService)
        {
            InitializeComponent();
            var vm = new OrdersControllerViewModel(emailSmtpService);
            this.DataContext = vm;
        }

        public Action<string> Navigator;
        public Action<string> PageNavigator;

        public void NavigationClick(object sender, RoutedEventArgs e)
        {
            var direction = (sender as Button).CommandParameter.ToString();
            Navigator?.Invoke(direction);
        }
        public void PageClick(object sender, RoutedEventArgs e)
        {
            var page = (sender as Button).CommandParameter as string;
            PageNavigator?.Invoke(page);
        }
    }
}
