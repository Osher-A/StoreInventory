using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreInventory.Services.MessageService
{
    public class MessageService : IMessageService
    {
        public  Func<string, string, Task>  OkMessageBoxEvent { get; set; }
        public  Func<string, string, Task<bool>> OkAndCancelMessageBoxEvent { get; set; }

    }
}
