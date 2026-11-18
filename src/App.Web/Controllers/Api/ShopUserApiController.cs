using System;
using System.Web.Http;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class ShopUserApiController : ApiControllerBase
    {

        private readonly IShopUserServiceHandler _shopUserServiceHandler;

        public ShopUserApiController(IShopUserServiceHandler shopUserServiceHandler)
        {
            _shopUserServiceHandler = shopUserServiceHandler;
        }

        [HttpGet]
        public IHttpActionResult LoginShopAgent(string email, string password, int shopId)
        {
            return Execute("LoginShopAgent", shopId, () => _shopUserServiceHandler.LoginShopAgent(email, password, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllBookings(int shopId)
        {
            return Execute(nameof(GetAllBookings), shopId, () => _shopUserServiceHandler.GetAllBookings(shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllCustomers(int shopId)
        {
            return Execute(nameof(GetAllCustomers), shopId, () => _shopUserServiceHandler.GetAllCustomers(shopId));
        }

        //[HttpPost]
        //public IHttpActionResult SaveShopUser(WsShopUser newShopUser, int shopId)
        //{
        //    return Execute("SaveShopUser", shopId, () => _shopUserServiceHandler.SaveShopUser(newShopUser,shopId));
        //}
        
        //[HttpGet]
        //public IHttpActionResult GetShopUserById(int userId, int shopId)
        //{
        //    return Execute("GetShopUserById", shopId, () => _shopUserServiceHandler.GetShopUserById(userId, shopId));
        //}

        [HttpDelete]
        public IHttpActionResult RemoveShopUseById(int userId, int shopId)
        {
            return Execute("RemoveShopUseById", shopId, () => _shopUserServiceHandler.RemoveShopUseById(userId, shopId));
        }

        [HttpGet]
        public IHttpActionResult UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus)
        {
            return Execute(nameof(UpdateUserBookingStatus), shopId, () => _shopUserServiceHandler.UpdateUserBookingStatus(bookingId, shopId, bookingStatus));
        }

        [HttpGet]
        public IHttpActionResult UpdateUserCredit(int customerId, int shopId, int credits)
        {
            return Execute(nameof(UpdateUserCredit), shopId, () => _shopUserServiceHandler.UpdateUserCredit(customerId, shopId, credits));
        }

        [HttpGet]
        public IHttpActionResult SendUserVoucher(int customerId, int shopId, int voucherId)
        {
            return Execute(nameof(SendUserVoucher), shopId, () => _shopUserServiceHandler.SendUserVoucher(customerId, shopId, voucherId));
        }

        [HttpGet]
        public IHttpActionResult GetUserFeedback(int shopId)
        {
            return Execute(nameof(GetUserFeedback), shopId, () => _shopUserServiceHandler.GetUserFeedback(shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllVoucherTypes(int shopId)
        {
            return Execute(nameof(GetAllVoucherTypes), shopId, () => _shopUserServiceHandler.GetAllVoucherTypes(shopId));
        }

        [HttpGet]
        public IHttpActionResult GetCustomerById(int customerId, int shopId)
        {
            return Execute(nameof(GetCustomerById), shopId, () => _shopUserServiceHandler.GetCustomerById(customerId, shopId));
        }

        [HttpPost]
        public IHttpActionResult SaveCustomer([FromBody]WsShopCustomer customer, [FromUri] int shopId)
        {
            return Execute(nameof(SaveCustomer), shopId, () => _shopUserServiceHandler.SaveCustomer(customer, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetVoucherTypeById(int id, int shopId)
        {
            return Execute(nameof(GetVoucherTypeById), shopId, () => _shopUserServiceHandler.GetVoucherTypeById(id, shopId));
        }

        [HttpPost]
        public IHttpActionResult SaveVoucherType([FromBody]WsVoucherType voucher, [FromUri]int shopId)
        {
            return Execute(nameof(SaveVoucherType), shopId, () => _shopUserServiceHandler.SaveVoucherType(voucher, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllShopService(int shopId)
        {
            return Execute(nameof(GetAllShopService), shopId, () => _shopUserServiceHandler.GetAllShopService(shopId));
        }

        [HttpGet]
        public IHttpActionResult GetShopServiceById(int id, int shopId)
        {
            return Execute(nameof(GetShopServiceById), shopId, () => _shopUserServiceHandler.GetShopServiceById(id, shopId));
        }

        [HttpPost]
        public IHttpActionResult SaveShopService([FromBody]WsShopService shopService, [FromUri]int shopId)
        {
            return Execute(nameof(SaveShopService), shopId, () => _shopUserServiceHandler.SaveShopService(shopService, shopId));
        }

        [HttpPost]
        public IHttpActionResult SaveShopUser(WsShopUser shopUser, int shopId)
        {
            return Execute(nameof(SaveShopUser), shopId, () => _shopUserServiceHandler.SaveShopUser(shopUser, shopId));
        }

        [HttpGet]
        public IHttpActionResult DeleteVoucherType(int id, int shopId)
        {
            return Execute(nameof(DeleteVoucherType), shopId, () => _shopUserServiceHandler.DeleteVoucherType(id, shopId));
        }

        [HttpGet]
        public IHttpActionResult DeleteShopService(int id, int shopId)
        {
            return Execute(nameof(DeleteShopService), shopId, () => _shopUserServiceHandler.DeleteShopService(id, shopId));
        }

        [HttpGet]
        public IHttpActionResult DeleteShopUser(int id, int shopId)
        {
            return Execute(nameof(DeleteShopUser), shopId, () => _shopUserServiceHandler.DeleteShopUser(id, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllShopUser(int shopId)
        {
            return Execute(nameof(GetAllShopUser), shopId, () => _shopUserServiceHandler.GetAllShopUser(shopId));
        }
        
        [HttpGet]
        public IHttpActionResult GetShopUserById(int id, int shopId)
        {
            return Execute(nameof(GetShopUserById), shopId, () => _shopUserServiceHandler.GetShopUserById(id, shopId));
        }

        [HttpGet]
        public IHttpActionResult GetShopServiceUser(int shopId)
        {
            return Execute(nameof(GetShopServiceUser), shopId, () => _shopUserServiceHandler.GetShopServiceUser(shopId));
        }
    }
}
