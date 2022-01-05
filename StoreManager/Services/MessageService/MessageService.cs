using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Services.MessageService
{
    public class MessageService : IMessageService
    {
        public  Func<string, string, Task>  OkMessageBoxEvent { get; set; }
        public  Func<string, string, Task<bool>> OkAndCancelMessageBoxEvent { get; set; }

        public async Task CustomersAddressDetailsMissingAlert()
        {
           await  OkMessageBoxEvent?.Invoke("Missing Details!", @"
         In Order to proceed, you'll have to provide the customer's Address 
         (i.e. House Number & Post code), or Email! ");
        }

        public void MissingDetailsAlert()
        {
            OkMessageBoxEvent?.Invoke("Missing Details!", "You forgot to fill in or select one of the boxes!");
        }

        public async Task<bool> DeleteWarningAlert()
        {
             var result = await OkAndCancelMessageBoxEvent("Warning!", "Are you sure you would like to delete this product!");
            return result;
        }
    }
}
