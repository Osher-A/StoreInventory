using StoreManager.DTO;

namespace StoreManager.Interfaces
{
    public interface IEmailSmtpService
    {
        void SendEmail(Order order);
    }
}