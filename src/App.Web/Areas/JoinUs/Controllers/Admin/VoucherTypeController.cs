using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;

namespace App.Web.Areas.JoinUs.Controllers.Admin
{
    public class VoucherTypeController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public VoucherTypeController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [AreaAuth]
        public ActionResult List()
        {
            var voucherTypes = _shopUserServiceClient.GetAllVoucherTypes(ShopId);
            
            return View(voucherTypes.Content);
        }

        [AreaAuth]
        public ActionResult Edit(int id)
        {
            ViewBag.result = string.Empty;

            var voucher = _shopUserServiceClient.GetVoucherTypeById(id, ShopId);

            return View(voucher.Content);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody]WsVoucherType voucher)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveVoucherType(voucher, ShopId);

            return View(voucher);
        }

        [AreaAuth]
        public ActionResult Add()
        {
            ViewBag.result = string.Empty;
            
            return View(new WsVoucherType());
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult Add([FromBody]WsVoucherType voucher)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveVoucherType(voucher, ShopId);

            return View(voucher);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.result = "Success";

            var result = _shopUserServiceClient.DeleteVoucherType(id, ShopId);
            
            return RedirectToAction("List");
        }
    }
}