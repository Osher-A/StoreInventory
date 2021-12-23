using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.ServicesTests.OrderServiceTests
{
    [TestFixture]
    internal class OrderDataServiceTests
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void SaveOrderDetails_WhenAllCustomersDetailsAreNullAndFullyPaid_DoNotAddNewCustomerInRepo()
        {

        }
       
        [Test]
        public void SaveOrderDetails_WhenPartOfDetailsNotNullAndCanBeMatchedToExistingCustomer_UpdateCustomerInRepo()
        {

        }
        [Test]
        public void SaveOrderDetails_WhenPartOfDetailsNotNullAndCantBeMatchedToExistingCustomer_AddNewCustomerInRepo()
        {

        }

    }
}
