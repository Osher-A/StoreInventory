using StoreInventory.DTO;
using StoreInventory.Enums;
using StoreInventory.Interfaces;
using StoreInventory.Services.MessageService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreInventory.Services.OrderServices
{
    public class PaymentService
    {
        private readonly IMessageService _messageService;
        private readonly ICustomer _customer;

        public bool IsDetailsValid { get; private set; }

        public PaymentService(IMessageService messageService, ICustomer customer)
        {
            _messageService = messageService;
            _customer = customer;
        }
        public PaymentAmounts PaymentController(PaymentStatus? ps, float totalCost)
        {
            PaymentAmounts pa = new PaymentAmounts();
            switch (ps)
            {
                case PaymentStatus.FullyPaid:
                    pa.AmountPaid = totalCost;
                    break;
                case PaymentStatus.NotPaid:
                    pa.AmountOwed = totalCost;
                    break;
            }
            return pa;
        }

        public async Task CustomersDetailsValidated(PaymentStatus? paymentStatus)
        {
            if ((paymentStatus == PaymentStatus.NotPaid || paymentStatus == PaymentStatus.PartlyPaid) && ((string.IsNullOrWhiteSpace(_customer.Address.House) || string.IsNullOrWhiteSpace(_customer.Address.Zip))
                && string.IsNullOrWhiteSpace(_customer.Email)))
            {
               await _messageService.OkMessageBoxEvent("Missing Details!", @"
In Order to proceed, you'll have to provide the customer's Address 
(i.e. House Number & Post code), or Email! ");
                IsDetailsValid = false;
            }
            else
                IsDetailsValid = true;
        }

        public PaymentStatus? UpdatePaymentStatus(bool addingItem, PaymentStatus? paymentStatus, PaymentAmounts paymentAmounts)
        {
            if (addingItem)
                if (paymentAmounts.AmountPaid > 0 && paymentAmounts.AmountOwed > 0)
                    return PaymentStatus.PartlyPaid;
                else
                    return paymentStatus;
            else
                return null; // this would make sure there is no checked radio button.
        }

        public float OutStandingBallance(float amountPaid, float totalCost)
        { 
                return totalCost - amountPaid;
        }
    }
}


