﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StoreInventory.Services.MessageService;
using StoreInventory.Services.ProductControllerServices;
using StoreInventory.ViewModel;
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
using ToastNotifications.Core;
using ToastNotifications.Lifetime.Clear;

namespace StoreInventory.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private OrdersControllerPage _mainWindowContentPage;
       // private OrderPage _mainWindowContentPage;
        private readonly IMessageService _messageService;
        private readonly ToastViewModel _toastVm;
        public MainWindow(IMessageService messageService)
        {
            InitializeComponent();
            _messageService = messageService;
            _messageService.OkMessageBoxEvent += MahAppsOKMessageBox;
            _messageService.OkAndCancelMessageBoxEvent += MahAppsOkAndCancelMessageBox;
            //_mainWindowContentPage = new OrderPage(messageService);
            _mainWindowContentPage = new OrdersControllerPage();

            _toastVm = new ToastViewModel();
            ToastService.ToastSuccessAction += ShowSuccess;
            ToastService.ToastErrorAction += ShowError;
        }

        private void ShowError(string message )
        {
            _toastVm.ShowError("ERROR!" + "  " + message );
        }

        private void ShowSuccess()
        {
            _toastVm.ShowSuccess("Email Sent!");
        }
        
        private async Task MahAppsOKMessageBox(string heading, string message)
        {
           await this.ShowMessageAsync(heading, message, MessageDialogStyle.Affirmative);
        }

        private async Task<bool> MahAppsOkAndCancelMessageBox(string heading, string message)
        {
            if ( await this.ShowMessageAsync(heading, message, MessageDialogStyle.AffirmativeAndNegative) == MessageDialogResult.Affirmative)
                return true;
            else
                return false;
        }

        private void MainWindowFrame_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = _mainWindowContentPage;

        }
    }
}
