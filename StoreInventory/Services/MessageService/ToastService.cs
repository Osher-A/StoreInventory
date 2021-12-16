using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInventory.Services.MessageService
{
    public static class ToastService
    {
        public static Action ToastSuccessAction { get; set; }
        public static Action ToastErrorAction { get; set;}

        public static void SuccessToast()
        {
            ToastSuccessAction?.Invoke();
        }
        public static void ErrorToast()
        {
            ToastErrorAction?.Invoke();
        }
    }
}
