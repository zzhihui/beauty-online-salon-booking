using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Service.Contract.Entities.Shop;
using App.Service.Services;
using App.Web.Application.Filter;

namespace App.Web.Controllers.Api
{
    [TokenValidation]
    public class ShopNewsApiController : ApiControllerBase
    {
        private readonly IShopNewsServiceHandler _shopNewsServiceHandler;

        public ShopNewsApiController(IShopNewsServiceHandler shopNewsServiceHandler)
        {
            _shopNewsServiceHandler = shopNewsServiceHandler;
        }


        [HttpGet]
        public IHttpActionResult GetShopNews(int shopId)
        {
            return Execute("GetShopNews", shopId, () => _shopNewsServiceHandler.GetShopNews(shopId));
        }


        [HttpPost]

        public IHttpActionResult SaveNews(WsShopNews selectedShopNews, int shopId)
        {
            return Execute("SaveNews", shopId, () => _shopNewsServiceHandler.SaveNews(selectedShopNews,shopId));
        }

        [HttpPut]
        public IHttpActionResult UpdateNews(WsShopNews selectedShopNews, int shopId)
        {
            return Execute("UpdateNews", shopId, () => _shopNewsServiceHandler.UpdateNews(selectedShopNews, shopId));
        }

        [HttpDelete]
        public IHttpActionResult RemoveShopNewsById(int id, int shopId)
        {
            return Execute("RemoveShopNewsById", shopId, () => _shopNewsServiceHandler.RemoveShopNewsById(id, shopId));
        }
    }
}
