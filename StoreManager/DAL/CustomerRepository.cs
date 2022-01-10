using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using StoreManager.Interfaces;
using StoreManager.Model;
using System.Collections.Generic;
using System.Linq;

namespace StoreManager.DAL
{
    internal class CustomerRepository : ICustomerRepository
    {
        public List<ICustomer> GetCustomers()
        {
            var customers = new List<ICustomer>();
            using (var db = new StoreContext())
            {
                customers = db.Customers
                    .Include(c => c.Orders)
                    .Include(c => c.Address)
                    .OrderBy(c => c.LastName)
                    .ToList<ICustomer>();
            }
            return customers;
        }

        public int GetLastCustomerId()
        {
            using (var db = new StoreContext())
            {
                return db.Customers
                         .ToList()
                         .Last()
                         .Id;
            }
        }

        public List<IOrder> GetCustomersOrder(int Id)
        {
            List<IOrder> customersOrder;
            using (var db = new StoreContext())
            {
                customersOrder = db.Orders.Where(o => o.CustomerId == Id)
                    .Include(o => o.OrdersProducts)
                    .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category)
                    .OrderBy(o => o.OrderDate)
                    .ToList<IOrder>();
            }
            return customersOrder;
        }

        public void AddNewCustomer(ICustomer newUiCustomer)
        {
            if(newUiCustomer.Address != null)
                newUiCustomer.Address.Id = NewAddressId(newUiCustomer.Address);

            Model.Customer modelCustomer = new Model.Customer();
            modelCustomer = (Model.Customer)(DTO.Customer)newUiCustomer;
            using (var db = new StoreContext())
            {
                db.Customers.Add(modelCustomer);
                db.SaveChanges();
            }
        }

        public void DeletingCustomer(int customerId)
        {
            using (var db = new StoreContext())
            {
                var customerToDelete = db.Customers.Find(customerId);
                db.Customers.Remove(customerToDelete);
            }
        }

        public void UpdateCustomer(ICustomer customerToEdit)
        {
            using (var db = new StoreContext())
            {
                var modelCustomer = db.Customers.Include(c => c.Address).First(c => c.Id == customerToEdit.Id);
                MapToModelCustomer(modelCustomer, customerToEdit);
                db.SaveChanges();
            }
        }

        private void MapToModelCustomer(Model.Customer modelCustomer, ICustomer uiCustomer)
        {
            modelCustomer.FirstNames = (!string.IsNullOrWhiteSpace(uiCustomer.FirstNames)) ? uiCustomer.FirstNames : modelCustomer.FirstNames;
            modelCustomer.LastName = (!string.IsNullOrWhiteSpace(uiCustomer.LastName)) ? uiCustomer.LastName : modelCustomer.LastName;
            modelCustomer.PhoneNumber = (!string.IsNullOrWhiteSpace(uiCustomer.PhoneNumber)) ? uiCustomer.PhoneNumber : modelCustomer.PhoneNumber;
            modelCustomer.Email = (!string.IsNullOrWhiteSpace(uiCustomer.Email)) ? uiCustomer.Email : modelCustomer.Email;
            UpdateAddress(modelCustomer, uiCustomer.Address);
        }

        private void UpdateAddress(Model.Customer modelCustomer, IAddress uiAddress)
        {
            var addressRepo = new AddressRepository();
            if (modelCustomer.Address != null && uiAddress.Id != 0)
                addressRepo.UpdateAddress(uiAddress);
            else if (uiAddress != null)
                modelCustomer.AddressId = NewAddressId(uiAddress);
        }

        private int NewAddressId(IAddress uiAddress)
        {
            var addressRepo = new AddressRepository();
            addressRepo.AddAddress(uiAddress);
            return addressRepo.GetLastAddressInserted();
        }
    }
}