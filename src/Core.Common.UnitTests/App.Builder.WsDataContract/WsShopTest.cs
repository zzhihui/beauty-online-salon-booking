using System;
using System.Collections.Generic;
using App.Service.Contract.Entities.Shop;
using Core.Common.Utility;
using Core.Test.Utilities.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Ploeh.AutoFixture;

namespace Core.Common.UnitTests.App.Builder.WsDataContract
{
    [TestClass]
    public class WsShopTest: AutoMockFixture<WsShop>
    {
        [TestMethod]
        public void JsonList()
        {

            var fixture = new Fixture();


            var shopList = new List<WsShop>()
            {

                fixture.Create<WsShop>(),
                fixture.Create<WsShop>(),
                fixture.Create<WsShop>(),
                fixture.Create<WsShop>(),
                fixture.Create<WsShop>(),

            };
            

            var jsonString = JsonConvert.SerializeObject(shopList);


            Assert.IsNotNull(jsonString);

        }
    }
}
