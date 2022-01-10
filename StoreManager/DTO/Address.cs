using StoreManager.Interfaces;
using System;
using System.Linq;

namespace StoreManager.DTO
{
    public class Address : IAddress
    {
        private string _street;
        private string _city;
        private bool _cityIsFirstSet = true; // To prevent stack from overflowing 
        private bool _streetIsFirstSet = true;// To prevent stack from overflowing
        public int Id { get; set; }
        public string House { get; set; }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                if(_streetIsFirstSet && !string.IsNullOrWhiteSpace(_street))
                GetHouseNumber();
            }
        }

        public string City
        {
            get
            { return _city; }
            set 
            { 
                _city = value; 
                if(_cityIsFirstSet && !string.IsNullOrWhiteSpace(_city))
                    GetZip();
            }
        }

        public string Zip { get; set; }
        public ICustomer Customer { get; set; }
        public string FirstLineOfAddress
        {
            get
            {
                return House + " " + Street;
            }
        }
        public string SecondLineOfAddress
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(City))
                    return City + " " + Zip;
                else
                    return Zip;
            }
        }

        public string WholeAddress
        {
            get
            {
                return $"{House} {Street} {City} {Zip}";
            }
        }

        private void GetHouseNumber()
        {
            _streetIsFirstSet = false;
            var words = Street.Split(' ');
            string house = "", street = "";
            foreach (var word in words)
            {
                if (word.Any(char.IsDigit) || word.Length == 1)
                    house += word;
                else
                    street += word + " ";
            }
            if(house.Length > 0)
               House = house;

            Street = street;
        }

        private void GetZip()
        {
            string zip = "", city = "";
            _cityIsFirstSet = false;
            var words = City.Split(' ');
            foreach (var word in words)
            {
                if (word.Any(char.IsDigit))
                    zip += word;
                else
                    city += word;
            }
            if(zip.Length > 0)
            Zip = zip;

            City = city;
        }

        public static explicit operator Address(Model.Address address)
        {
            return new Address
            {
                Id = address.Id,
                House = address.House,
                Street = address.Street,
                Zip = address.Zip,
                City = address.City
            };
        }

        public static explicit operator Model.Address(Address address)
        {
            return new Model.Address
            {
                House = address.House,
                Street = address.Street,
                Zip = address.Zip,
                City = address.City,
               // CustomerId = address.Customer.Id
            };
        }
    }
}