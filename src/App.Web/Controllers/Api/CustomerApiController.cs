using System;
using System.Web.Http;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{

    [TokenValidation]
    public class CustomerApiController : ApiControllerBase
    {
        private readonly ICustomerServiceHandler _customerServiceHandler;
        
        public CustomerApiController(ICustomerServiceHandler customerServiceHandler)
        {
            _customerServiceHandler = customerServiceHandler;
        }

        [HttpGet]
        public IHttpActionResult LoginCustomer(string email, string password, int shopId)
        {
            return Execute("LoginCustomer", shopId, () => _customerServiceHandler.LoginCustomer(email, password, shopId));
        }

        [HttpPost]
        public IHttpActionResult RegisterCustomer([FromBody]WsShopCustomer customer, [FromUri]int shopId)
        {
            return Execute("RegisterCustomer", shopId, () => _customerServiceHandler.RegisterCustomer(customer, shopId));
        }

        [HttpPost]
        public IHttpActionResult AddCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute("AddCustomer", shopId, () => _customerServiceHandler.AddCustomer(customer, shopId));
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(WsShopCustomer customer, int shopId)
        {
            return Execute("UpdateCustomer", shopId, () => _customerServiceHandler.UpdateCustomer(customer, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllCustomers(int shopId)
        {
            return Execute("GetAllCustomers", shopId, () => _customerServiceHandler.GetAllCustomers(shopId));
        }

        [HttpGet]
        public IHttpActionResult GetCustomerById(int id, int shopId)
        {
            return Execute("GetCustomerById", shopId, () => _customerServiceHandler.GetCustomerById(id, shopId));
        }

        [HttpGet]
        public IHttpActionResult PasswordRecovery(string email, int shopId)
        {
            return Execute("CustomerRecovery", shopId, () => _customerServiceHandler.SendRecoveryEmail(email, shopId));
        }

        [HttpDelete]
        public IHttpActionResult RemoveCustomerById(int customerId, int shopId)
        {
            return Execute("RemoveCustomerById", shopId, () => _customerServiceHandler.RemoveCustomerById(customerId, shopId));
        }

        [HttpPost]
        public IHttpActionResult SaveContactForm([FromBody]WsContactForm contactForm, [FromUri]int shopId)
        {
            return Execute(nameof(SaveContactForm), shopId, () => _customerServiceHandler.SaveContactForm(contactForm, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetBookingHistory(int customerId, int shopId)
        {
            return Execute(nameof(GetBookingHistory), shopId, () => _customerServiceHandler.GetBookingHistory(customerId, shopId));
        }

        [HttpPut]
        public IHttpActionResult CancelBooking(int customerId, int shopId, int bookingId)
        {
            return Execute(nameof(CancelBooking), shopId, () => _customerServiceHandler.CancelBooking(customerId, shopId, bookingId));
        }
    }
}
