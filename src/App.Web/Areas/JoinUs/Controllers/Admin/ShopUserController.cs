using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Authentication;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;

namespace App.Web.Areas.JoinUs.Controllers.Admin
{
    public class ShopUserController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public ShopUserController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [AreaAuth]
        public ActionResult List()
        {
            //var shopUsers = _shopUserServiceClient.GetAllShopUsers(ShopId);

            return View();
        }

        [AreaAuth]
        public ActionResult Edit(int id)
        {
            ViewBag.result = string.Empty;

            var shopUser = _shopUserServiceClient.GetShopUserById(id, ShopId);

            return View(shopUser.Content);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit([FromBody]WsShopUser shopUser)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveShopUser(shopUser, ShopId);

            return View(shopUser);
        }

        [AreaAuth]
        public ActionResult Add()
        {
            ViewBag.result = string.Empty;

            return View(new WsShopUser());
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult Add([FromBody]WsShopUser shopUser)
        {
            ViewBag.result = "Success";
            var result = _shopUserServiceClient.SaveShopUser(shopUser, ShopId);

            return View(shopUser);
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {
            ViewBag.result = "Success";

            var result = _shopUserServiceClient.DeleteShopUser(id, ShopId);

            return RedirectToAction("List");
        }
    }
}