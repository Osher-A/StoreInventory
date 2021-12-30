using StoreInventory.DTO;

namespace StoreInventory.Interfaces
{
    public interface IEmailSmtpService
    {
        void SendEmail(Order order);
    }
}