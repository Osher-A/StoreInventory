using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Services.MessageService
{
    public static class ToastService
    {
        public static Action ToastSuccessAction { get; set; }
        public static Action<string> ToastErrorAction { get; set;}

        public static void SuccessToast()
        {
            ToastSuccessAction?.Invoke();
        }
        public static void ErrorToast(string message)
        {
            ToastErrorAction?.Invoke(message);
        }
    }
}
