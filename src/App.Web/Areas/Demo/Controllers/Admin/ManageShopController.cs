using System.Web.Mvc;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;

namespace App.Web.Areas.Demo.Controllers.Admin
{
    public class ManageShopController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public ManageShopController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [HttpPost]
        public ActionResult UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus)
        {
            var result = _shopUserServiceClient.UpdateUserBookingStatus(bookingId, shopId, bookingStatus);

            return new JsonResult {Data = result};
        }

        [HttpPost]
        public ActionResult UpdateUserCredit(int customerId, int shopId, int credits)
        {
            var result = _shopUserServiceClient.UpdateUserCredit(customerId, shopId, credits);

            return new JsonResult { Data = result };
        }

        [HttpPost]
        public ActionResult SendUserVoucher(int customerId, int shopId, int voucherId)
        {
            var result = _shopUserServiceClient.SendUserVoucher(customerId, shopId, voucherId);

            return new JsonResult { Data = result };
        }
    }
}