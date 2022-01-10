using Microsoft.EntityFrameworkCore.Infrastructure;
using StoreManager.Interfaces;
using StoreManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreManager.DAL
{
    public class AddressRepository
    {
        public void AddAddress(IAddress customersAddress)
        {
            Model.Address address = (Model.Address)(DTO.Address)customersAddress;
            using var db = new StoreContext();
            db.Addresses.Add(address);
            db.SaveChanges();
        }

        public void UpdateAddress(IAddress customersAddress)
        {
            using var db = new StoreContext();
            var modelAddress = db.Addresses.SingleOrDefault(a => a.Id == customersAddress.Id);
            UiAddressModification(modelAddress, customersAddress);
            db.SaveChanges();
        }

        public IAddress GetAddressById(int id)
        {
            using var db = new StoreContext();
            return db.Addresses.Find(id);
        }

        public int GetLastAddressInserted()
        {
            using var db = new StoreContext();
            return db.Addresses.ToList().Last().Id;
        }

        private void UiAddressModification(Address modelAddress, IAddress uiAddress)
        {
                modelAddress.House = (!string.IsNullOrWhiteSpace(uiAddress.House)) ? uiAddress.House : modelAddress.House;
                modelAddress.Street = (!string.IsNullOrWhiteSpace(uiAddress.Street)) ? uiAddress.Street : modelAddress.Street;
                modelAddress.Zip = (!string.IsNullOrWhiteSpace(uiAddress.Zip)) ? uiAddress.Zip : modelAddress.Zip;
                modelAddress.City = (!string.IsNullOrWhiteSpace(uiAddress.Zip)) ? uiAddress.City : modelAddress.City;
        }
    }
}
