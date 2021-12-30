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
    /// Interaction logic for MainWindowContentPage.xaml
    /// </summary>
    public partial class MainWindowContentPage : Page
    {
        public MainWindowContentPage()
        {
            InitializeComponent();
        }

        public Action<string> TileClick;

        private void OnTileClick(object sender, RoutedEventArgs e)
        {
           var tile = sender as MahApps.Metro.Controls.Tile;
            
           TileClick?.Invoke(tile.Name);
        }
    }
}
