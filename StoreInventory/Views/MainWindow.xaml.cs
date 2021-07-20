using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StoreInventory.Services;
using StoreInventory.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreInventory.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private StockPage _mainWindowContentPage = new StockPage();
        public MainWindow()
        {
            InitializeComponent();
            var productService = new ProductService();
            ProductService.MessageBoxEvent += MahAppsMessageBox;
        }

        private async void MahAppsMessageBox(string message)
        {
            await this.ShowMessageAsync("Missing Details!", message, MessageDialogStyle.Affirmative);
        }

        private void MainWindowFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = _mainWindowContentPage;

        }
    }
}
