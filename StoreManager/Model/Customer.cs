using System;
using System.Collections.Generic;
using System.Text;
using MyLibrary.Utilities;
using StoreManager.Interfaces;

namespace StoreManager.Model
{
    public class Customer : IMapper, ICustomer
    {
        public int Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IAddress Address { get; set; }
        public int AddressId { get; set; }
        public List<Order> Orders { get; set; }


        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
