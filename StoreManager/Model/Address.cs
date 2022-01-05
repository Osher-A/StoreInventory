using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManager.Model
{
    public class Address : IAddress
    {
        public int Id { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
        public string  City { get; set; }
        public string Zip { get; set; }

        public ICustomer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
