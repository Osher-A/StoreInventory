using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.ComponentModel;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace StoreManager.ViewModel
{
    public class ToastViewModel
    {
        private Notifier _notifier;
        private WindowPositionProvider _windowPositionProvider;

        public ToastViewModel()
        {
            PositionToastAtTop();
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = _windowPositionProvider; 

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(6),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(6));

                cfg.Dispatcher = Application.Current.Dispatcher;

                cfg.DisplayOptions.TopMost = false;
                cfg.DisplayOptions.Width = 250;
            });

            _notifier.ClearMessages(new ClearAll());
        }

        public void OnUnloaded()
        {
            _notifier.Dispose();
        }

        public void ShowInformation(string message)
        {
            PositionToastAtBottom();
            _notifier.ShowInformation(message);
            PositionToastAtTop();
        }

        public void ShowSuccess(string message)
        {
            _notifier.ShowSuccess(message);
        }

        public void ShowWarning(string message)
        {
            _notifier.ShowWarning(message);
        }

        public void ShowError(string message)
        {
            _notifier.ShowError(message);
        }

        private void PositionToastAtTop()
        {
            _windowPositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 25,
                    offsetY: 100);
        }

        private void PositionToastAtBottom()
        {
            _windowPositionProvider = new WindowPositionProvider(
                   parentWindow: Application.Current.MainWindow,
                   corner: Corner.BottomCenter,
                   offsetX: 25,
                   offsetY: 100);
        }

    }
}