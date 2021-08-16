using System.Collections.Generic;

namespace StoreInventory.Interfaces
{
    public interface ICustomer
    {
        string Email { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string PhoneNumber { get; set; }
    }
}