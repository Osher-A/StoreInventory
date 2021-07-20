using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary.Utilities;

namespace StoreInventory.Model
{
     public class Customer : IMapper
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
