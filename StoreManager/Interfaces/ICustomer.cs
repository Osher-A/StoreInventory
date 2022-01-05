using System.Collections.Generic;

namespace StoreManager.Interfaces
{
    public interface ICustomer
    {
        string Email { get; set; }
        int Id { get; set; }
        string FirstNames { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        IAddress Address { get; set; }
    }
}