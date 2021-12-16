using StoreInventory.Interfaces;
using System;
using System.Linq;

namespace StoreInventory.DTO
{
    public class Address : IAddress
    {
        private string _street;
        private string _city;
        private bool _isFirstSet = true;
        public int Id { get; set; }
        public string House { get; set; }

        public string Street
        {
            get { return _street; }
            set
            {
                _street = value;
                if(!string.IsNullOrWhiteSpace(_street))
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
                if(_isFirstSet && !string.IsNullOrWhiteSpace(_city))
                    SetZip();
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
            var words = Street.Split(' ');
            string house = "";
            foreach (var word in words)
            {
                if (word.Any(char.IsDigit) || word.Length == 1)
                    house += word;
            }
            House = house;
        }

        private void SetZip()
        {
            string _zip = "";
            _isFirstSet = false;
            var words = City.Split(' ');
            foreach (var word in words)
            {
                if (word.Any(char.IsDigit))
                    _zip += word;
            }
            if (_zip.Length == City.Length)
                City = String.Empty;

            Zip = _zip;
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
                City = address.City
            };
        }
    }
}