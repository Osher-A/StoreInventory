using StoreInventory.Enums;
using StoreInventory.Services.ProductControllerServices;
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
    /// Interaction logic for ProductControllerPage.xaml
    /// </summary>
    public partial class ProductControllerPage : Page
    {
        public ProductControllerPage()
        {
            InitializeComponent();
        }
        
        public Action<string> Navigator { get; set; }
        public Action<string> PageNavigator { get; set; }

        public void NavigationClick(object sender, RoutedEventArgs e)
        {
            var direction = (sender as Button).CommandParameter as string;
            Navigator?.Invoke(direction);
        }

        public void PageClick(object sender, RoutedEventArgs e)
        {
            var page = (sender as Button).CommandParameter as string;
            PageNavigator?.Invoke(page);
        }

    }
}
