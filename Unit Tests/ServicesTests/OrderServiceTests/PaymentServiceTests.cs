using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;
using StoreManager.DTO;
using StoreManager.Enums;
using StoreManager.Services.MessageService;
using StoreManager.Services.OrderServices;

namespace UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<IMessageService> _mockMessageService;
        private PaymentService _paymentService;
        private Customer _customer;
        [SetUp]
        public void SetUp()
        {
            _mockMessageService = new Mock<IMessageService>();
            _customer = new Customer() { Address = new Address { House = "10", Street = "Park Road", City = "Manchester", Zip = "M20 OLD" }, Email = "oa@gmail.com" };
            _paymentService = new PaymentService(_mockMessageService.Object, _customer);
        }

        [Test]
        public void PaymentController_WhenPaymentStatusIsFullyPaid_ReturnsAmountPaidEqualsTotalCost()
        {
            PaymentAmounts pa = _paymentService.PaymentController(PaymentStatus.FullyPaid, 43.20f);

            Assert.That(pa.AmountPaid == 43.20f);
        }

        [Test]
        public void PaymentController_WhenPaymentStatusIsNotPaid_ReturnsAmountOwedEqualsTotalCost()
        {
            PaymentAmounts pa = _paymentService.PaymentController(PaymentStatus.NotPaid, 43.20f);

            Assert.That(pa.AmountOwed == 43.20f);
        }

        [Test]
        public void PaymentController_WhenPaymentStatusIsPartlyPaid_AmountPaidAndOwedDoesNotGetUpdated()
        {
            PaymentAmounts pa = _paymentService.PaymentController(PaymentStatus.PartlyPaid, 43.20f);

            Assert.That(pa.AmountPaid == 0);
            Assert.That(pa.AmountOwed == 0);
        }

        [Test]
        [TestCase (PaymentStatus.NotPaid, "10", "Park Road","Manchester","", "")]
        [TestCase (PaymentStatus.PartlyPaid, "10", "Park Road","Manchester", "", "")]
        [TestCase (PaymentStatus.NotPaid, "", "Park Road","Manchester", "M20 OLD", "")]
        [TestCase (PaymentStatus.PartlyPaid, "", "Park Road","Manchester", "M20 OLD", "")]
        public void CustomersDetailsValidated_NotFullyPaidWithNoValidDetails_AnErorrMessageIsDisplayed(PaymentStatus paymentStaus, string house, string street, string city, string zip, string email)
        {
            _customer.Address.House = house;
            _customer.Address.Street = street;
            _customer.Address.City = city;
            _customer.Address.Zip = zip;
            _customer.Email = email;

            _paymentService.CustomersDetailsValidated(paymentStaus);

            Assert.That(_paymentService.IsDetailsValid == false);
            _mockMessageService.Verify(mms => mms.CustomersAddressDetailsMissingAlert(), Times.Once);
        }

        [Test]
        [TestCase (PaymentStatus.NotPaid, "10", "","", "M20 OLD", "")]
        [TestCase (PaymentStatus.PartlyPaid, "10", "","", "M20 OLD", "")]
        [TestCase (PaymentStatus.NotPaid, "", "", "", "", "oa@gmail.com")]
        [TestCase (PaymentStatus.PartlyPaid, "", "", "","", "oa@gmail.com")]
        public void CustomersDetailsValidated_NotFullyPaidButValidDetails_AnErorrMessageIsDisplayed(PaymentStatus paymentStatus, string house, string street, string city, string zip, string email)
        {
            _customer.Address.House = house;
            _customer.Address.Street = street;
            _customer.Address.Zip = zip;
            _customer.Address.City = city;
            _customer.Email = email;

            _paymentService.CustomersDetailsValidated(paymentStatus);

            Assert.That(_paymentService.IsDetailsValid == true);
            _mockMessageService.Verify(mms => mms.CustomersAddressDetailsMissingAlert(), Times.Never());
        }

         [Test]
        public void CustomersDetailsValidated_FullyPaid_NoErorrMessageIsDisplayed()
        {
            _customer.Address.House = "";
            _customer.Address.Zip = "";
            _customer.Email = "";

            _paymentService.CustomersDetailsValidated(PaymentStatus.FullyPaid);

            Assert.That(_paymentService.IsDetailsValid == true);
            _mockMessageService.Verify(mms => mms.CustomersAddressDetailsMissingAlert(), Times.Never());
        }

        [Test]
        public void UpdatePaymentStatus_WhenAddingItemAndStatusIsFullyPaid_ReturnStatusOfPartlyPaid()
        {
            var status = _paymentService.UpdatePaymentStatus(true, PaymentStatus.FullyPaid);

            Assert.That(status, Is.EqualTo(PaymentStatus.PartlyPaid));
        }

        [Test]
        public void UpdatePaymentStatus_WhenAddingItemAndStatusIsNotFullyPaid_ReturnSameStatus()
        {
            var status = _paymentService.UpdatePaymentStatus(true, PaymentStatus.PartlyPaid);
            var status2 = _paymentService.UpdatePaymentStatus(true, PaymentStatus.NotPaid);

            Assert.That(status, Is.EqualTo(PaymentStatus.PartlyPaid));
            Assert.That(status2, Is.EqualTo(PaymentStatus.NotPaid));
        }

        [Test]
        public void UpdatePaymentStatus_WhenRemovingAItemAndStatusIsFullyPaid_ReturnSameStatus()
        {
            var status = _paymentService.UpdatePaymentStatus(false, PaymentStatus.FullyPaid);

            Assert.That(status, Is.EqualTo(PaymentStatus.FullyPaid));
        }

        [Test]
        public void UpdatePaymentStatus_WhenRemovingAItemAndStatusIsNotFullyPaid_ReturnStatusAsNull()
        {
            var status = _paymentService.UpdatePaymentStatus(false, PaymentStatus.PartlyPaid);
            var status2 = _paymentService.UpdatePaymentStatus(false, PaymentStatus.NotPaid);

            Assert.That(status == null);
            Assert.That(status2 == null);
        }

        [Test]
        public void OutStandingBallance_WhenCalled_ReturnsBallance()
        {
            var ballance = _paymentService.OutStandingBallance(50, 101);

            Assert.That(ballance == 51f);
        }
    }
}
