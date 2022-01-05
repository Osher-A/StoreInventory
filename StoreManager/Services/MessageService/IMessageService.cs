using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreManager.Services.MessageService
{
    public interface IMessageService
    {
        Func<string, string, Task> OkMessageBoxEvent { get; set; }
        public Func<string, string, Task<bool>> OkAndCancelMessageBoxEvent { get; set; }

        Task CustomersAddressDetailsMissingAlert();
        void MissingDetailsAlert();
        Task<bool> DeleteWarningAlert();
    }
}
