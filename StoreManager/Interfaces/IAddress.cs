using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Interfaces
{
        public interface IAddress
        {
            string House { get; set; }
            int Id { get; set; }
            string Street { get; set; }
            string Zip { get; set; }
           ICustomer Customer { get; set; }
        string City { get; set; }
    }
}
