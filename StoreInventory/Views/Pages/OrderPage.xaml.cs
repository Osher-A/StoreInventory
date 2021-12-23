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
        public OrderPage(IMessageService messageService, IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            InitializeComponent();

            var vm = new OrderViewModel(messageService, orderRepository, customerRepository);
            this.DataContext = vm;

            vm.ScrollUpEvent += Vm_ScrollUpEvent;
        }

        private void Vm_ScrollUpEvent(object sender, EventArgs e)
        {
            this.scrollViewer.ScrollToTop();
        }
    }
}
