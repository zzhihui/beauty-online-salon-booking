using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;
using App.Web.ViewModels;

namespace App.Web.Areas.Demo.Controllers.Admin
{
    public class ScheduleController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public ScheduleController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient = new ShopUserServiceClient();
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult EditAppointment([FromBody]WsSchedule appointment)
        {
            var result = _shopUserServiceClient.SaveAppointment(appointment, ShopId);
            
            return new JsonResult { Data = result };
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult DeleteAppointment([FromBody]WsSchedule appointment)
        {
            var result = _shopUserServiceClient.DeleteAppointment(appointment, ShopId);

            return new JsonResult { Data = result };
        }

        [AreaAuth]
        [System.Web.Mvc.HttpPost]
        public ActionResult AddAppointment([FromBody]WsSchedule appointment)
        {           
            var result = _shopUserServiceClient.AddAppointment(appointment, ShopId);

            return new JsonResult { Data = result };
        }
    }
}