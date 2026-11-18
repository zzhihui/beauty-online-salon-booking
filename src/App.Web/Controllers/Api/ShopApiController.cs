using System;
using System.Web.Http;
using App.Service.Contract.Entities.Shop;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class ShopApiController : ApiControllerBase
    {

        private readonly IShopServiceHandler _shopServiceHandler;

        public ShopApiController(IShopServiceHandler shopServiceHandler)
        {
            _shopServiceHandler = shopServiceHandler;
        }
        
        [HttpGet]
        public IHttpActionResult GetShopInfo(int shopId)
        {
            return Execute("GetShopInfo", shopId, () => _shopServiceHandler.GetShopInfo(shopId));
        }
        
        [HttpPut]
        public IHttpActionResult UpdateShopInfo(WsShop shopInfo, int shopId)
        {
            return Execute("UpdateShopInfo", shopId, () => _shopServiceHandler.UpdateShopInfo(shopInfo,shopId));
        }

        [HttpGet]
        public IHttpActionResult GetAllServices(int shopId)
        {
            return Execute(nameof(GetAllServices), shopId, () => _shopServiceHandler.GetAllServices(shopId));
        }

        [HttpGet]
        public IHttpActionResult GetUniqueServiceCategories(int shopId)
        {
            return Execute(nameof(GetUniqueServiceCategories), shopId, () => _shopServiceHandler.GetAllServiceCategories(shopId));
        }

        [HttpPost]
        public IHttpActionResult BookService([FromBody]WsBooking booking, [FromUri]int shopId)
        {
            return Execute(nameof(BookService), shopId, () => _shopServiceHandler.BookService(booking));
        }

        [HttpGet]
        public IHttpActionResult GetAvailableSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId)
        {
            return Execute(nameof(GetAvailableSlots), shopId, () => _shopServiceHandler.GetAvailableSlots(shopId, startDateTime, serviceId, shopUserId));
        }

        [HttpGet]
        public IHttpActionResult GetSchedules(int shopId)
        {
            return Execute(nameof(GetAvailableSlots), shopId, () => _shopServiceHandler.GetSchedules(shopId));
        }

        [HttpPost]
        public IHttpActionResult SaveAppointment([FromBody]WsSchedule schedule, [FromUri]int shopId)
        {
            return Execute(nameof(BookService), shopId, () => _shopServiceHandler.SaveAppointment(schedule));
        }

        [HttpPost]
        public IHttpActionResult DeleteAppointment([FromBody]WsSchedule schedule, [FromUri]int shopId)
        {
            return Execute(nameof(BookService), shopId, () => _shopServiceHandler.DeleteAppointment(schedule));
        }

        [HttpPost]
        public IHttpActionResult AddAppointment([FromBody]WsSchedule schedule, [FromUri]int shopId)
        {
            return Execute(nameof(BookService), shopId, () => _shopServiceHandler.Addppointment(schedule));
        }
    }
}
