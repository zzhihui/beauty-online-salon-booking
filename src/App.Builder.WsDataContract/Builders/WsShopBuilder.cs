using System;
using System.Collections.Generic;
using App.Service.Contract.Entities.Shop;

namespace App.Builder.WsDataContract.Builders
{
    public class WsShopBuilder
    {
        private readonly WsShop _entity;

        public WsShopBuilder()
        {
            _entity = new WsShop
            {
                Name = "Ichiban Sushi",
                ShopNumber = "(0)15988109",
                ShopMobile = "0870966688",
                Address1 = "139 Parnell Street Dublin 1",
                Address2 = "Dublin",
             
            };
        }

       

        public WsShop Build()
        {
            return _entity;
        }



    }
}