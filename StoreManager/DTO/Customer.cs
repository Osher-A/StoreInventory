using MyLibrary.Utilities;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StoreManager.DTO 
{
    public class Customer : ICustomer , INotifyPropertyChanged 
    {
        private string _firstNames;
        private string _lastName;
        private string _phoneNumber;
        private string _email;
        private IAddress _address;

        public int Id { get; set; }
        public string FirstNames
        {
            get { return _firstNames; }
            set
            {
                _firstNames = value;
                OnPropertyChanged(nameof(FirstNames));
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set 
            { 
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string FullName 
        {
            get 
            { 
                if(!string.IsNullOrWhiteSpace(FirstNames))
                return (FirstNames + " " + LastName).Trim();
                else
                    return LastName;
            }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set 
            { 
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        public string Email
        {
            get { return _email; }
            set 
            { 
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public IAddress Address
        {
            get { return _address; }
            set
            { 
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static explicit operator Customer(Model.Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                FirstNames = customer.FirstNames,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
        }
       
        public static explicit operator Model.Customer(Customer customer)
        {
            return new Model.Customer
            {
                FirstNames = customer.FirstNames,
                LastName = customer.LastName,
                Address = (Model.Address)(DTO.Address)customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email
            };
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
