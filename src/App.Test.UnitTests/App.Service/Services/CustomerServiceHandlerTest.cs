using System;
using App.Service.Services;
using Core.Test.Utilities.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Test.UnitTests.App.Service.Services
{
    [TestClass]
    public class CustomerServiceHandlerTest: AutoMockFixture<CustomerServiceHandler>
    {
        [TestMethod]
        public void LoginCustomerVerify()
        {
            //var email = "dummyemail";
            //var password = "dummyPassword";
            //var shopId = 1;


            //var act = Sut.LoginCustomer(email, password, shopId);


        }
        [TestMethod]
        public void LoginCustomerFailValidation()
        {

        }

        [TestMethod]
        public void LoginCustomerFailBusinessEngineException()
        {

        }
    }
}
