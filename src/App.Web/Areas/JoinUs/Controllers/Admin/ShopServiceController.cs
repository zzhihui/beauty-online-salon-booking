using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;

namespace App.Web.Areas.JoinUs.Controllers.Admin
{
    public class ShopServiceController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public ShopServiceController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [AreaAuth]
        public ActionResult List()
        {
            //var shopServices = _shopUserServiceClient.GetAllShopServices(ShopId);

            return View();
        }

        [AreaAuth]
        public ActionResult Edit(int id)
        {
            ViewBag.result = string.Empty;

            var shopService = _shopUserServiceClient.GetShopServiceById(id, ShopId);

            return View(shopService.Content);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody]WsShopService shopService)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveShopService(shopService, ShopId);

            return View(shopService);
        }

        [AreaAuth]
        public ActionResult Add()
        {
            ViewBag.result = string.Empty;

            return View(new WsShopService());
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult Add([FromBody]WsShopService shopService)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveShopService(shopService, ShopId);

            return View(shopService);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.result = "Success";

            var result = _shopUserServiceClient.DeleteShopService(id, ShopId);

            return RedirectToAction("List");
        }
    }
}