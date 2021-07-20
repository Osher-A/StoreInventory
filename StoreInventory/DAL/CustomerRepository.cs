using MyLibrary.Utilities;
using Microsoft.EntityFrameworkCore;
using StoreInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreInventory.DAL
{
    class CustomerRepository
    {
        public List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();
            using (var db = new StoreContext())
            {
                customers = db.Customers
                    .Include(c => c.Orders)
                    .OrderBy(c => c.Name)
                    .ToList();
            }
            return customers;
        }

        public List<Order> GetCustomersOrder(int Id)
        {
            List<Order> customersOrder;
            using (var db = new StoreContext())
            {
                customersOrder = db.Orders.Where(o => o.CustomerId == Id)
                    .Include(o => o.OrdersProducts)
                    .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.Category)
                    .OrderBy(o => o.OrderDate)
                    .ToList();
            }
            return customersOrder;
        }

        public void AddingCustomer(DTO.Customer newDTOCustomer)
        {
            var modelCustomer = MyMapper.Mapper(newDTOCustomer, new Customer());
            using(var db = new StoreContext())
            {
                db.Customers.Add(modelCustomer);
                db.SaveChanges();
            }
        }

        public void EditingCustomer(DTO.Customer customerToEdit)
        {
            using(var db = new StoreContext())
            {
                var modelCustomer = db.Customers.Find(customerToEdit.Id);
                MyMapper.Mapper(customerToEdit, modelCustomer);
                db.SaveChanges();
            }
        }

        public void DeletingCustomer(int customerId)
        {
            using(var db = new StoreContext())
            {
                var customerToDelete = db.Customers.Find(customerId);
                db.Customers.Remove(customerToDelete);
            }
        }
    }
}
