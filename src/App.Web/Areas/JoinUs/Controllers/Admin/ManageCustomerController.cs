using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Authentication;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;
using App.Web.ViewModels;

namespace App.Web.Areas.JoinUs.Controllers.Admin
{
    public class ManageCustomerController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public ManageCustomerController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [AreaAuth]
        public ActionResult List()
        {
            //Get All Bookings From DB
            var customers = _shopUserServiceClient.GetAllCustomers(ShopId);

            var voucherTypes = _shopUserServiceClient.GetAllVoucherTypes(ShopId);

            var model = new CustomerViewModel { Customers = customers.Content, VoucherTypes = voucherTypes.Content };

            return View(model);
        }

        [AreaAuth]
        public ActionResult Edit(int id)
        {
            ViewBag.result = string.Empty;

            var customer = _shopUserServiceClient.GetCustomerById(id, ShopId);
            
            return View(customer.Content);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody]WsShopCustomer customer)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveCustomer(customer, ShopId);

            return View(customer);
        }
    }
}