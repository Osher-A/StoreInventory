using MyLibrary.Extentions;
using StoreManager.DAL;
using StoreManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StoreManager.Services.OrderControllerServices
{
    internal class OrderSearchService
    {
        private IOrderRepository _orderRepository;
        public ObservableCollection<DTO.Order> AllOrders { get; private set; }

        public OrderSearchService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            AllOrders = MapToDTOOrders().ToObservableCollection();
        }

        public ObservableCollection<DTO.Order> GetUnPaidOrders()
        {
            return AllOrders.Where(ao => ao.AmountPaid < ao.Total).ToObservableCollection();
        }
        public ObservableCollection<DTO.Order> SearchByDate(DateTime date)
        {
            return AllOrders.Where(ao => ao.OrderDate.Date == date.Date)
                           .OrderByDescending(ao => ao.OrderDate)
                           .ToObservableCollection();
        }

        public ObservableCollection<DTO.Order> SearchByName(string name)
        {
            var searchOrders = new ObservableCollection<DTO.Order>();
            var nameOrders = new List<DTO.Order>();

            if (AllOrders.Any(ao => ((DTO.Customer)ao.Customer).FullName != null)) 
                nameOrders = AllOrders.Where(ao => ((DTO.Customer)ao.Customer).FullName != null).ToList();

            if(nameOrders.Count > 0)
                if (nameOrders.Any(ao => ((DTO.Customer)ao.Customer).FullName.ToLower().Contains(name.ToLower())))
                   searchOrders = nameOrders.Where(ao => ((DTO.Customer)ao.Customer).FullName.ToLower().Contains(name.ToLower()))
                       .OrderBy(ao => ((DTO.Customer)ao.Customer).FullName)
                       .ToObservableCollection();
           
            return searchOrders;
        }

        public ObservableCollection<DTO.Order> SearchByEmail(string email)
        {
            var searchOrders = new ObservableCollection<DTO.Order>();
            var emailOrders = new List<DTO.Order>();

            if (AllOrders.Any(ao => ao.Customer.Email != null))
              emailOrders = AllOrders.Where(ao => ao.Customer.Email != null).ToList();

            if (emailOrders.Any(ao => ao.Customer.Email.Contains(email)))
                  searchOrders = emailOrders.Where(ao => ao.Customer.Email.Contains(email)).ToObservableCollection();
                
            return searchOrders;
        }

        public ObservableCollection<DTO.Order> SearchByAddress(string address)
        {
            var searchOrders = new ObservableCollection<DTO.Order>();
            List<DTO.Order> addressOrders = new List<DTO.Order>();

            if (AllOrders.Any(ao => ao.Customer.Address != null))
              addressOrders = AllOrders.Where(ao => ao.Customer.Address != null).ToList();

               if(addressOrders.Any(ao => ((DTO.Address)ao.Customer.Address).WholeAddress.ToLower().Contains(address.ToLower())))
                searchOrders = AllOrders.Where(ao => ((DTO.Address)ao.Customer.Address).WholeAddress.ToLower().Contains(address.ToLower()))
                    .OrderBy(ao => ((DTO.Customer)ao.Customer).FullName)
                    .ToObservableCollection();

                return searchOrders;
        }

        private IEnumerable<DTO.Order> MapToDTOOrders()
        {
            foreach(var order in _orderRepository.GetOrders())
            {
                yield return (DTO.Order)(Model.Order)order;
            }
        }

    }
}
