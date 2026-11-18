using System.Collections.Generic;
using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
using App.Service.Transformers;
using App.Service.Transformers.In;
using App.Service.Transformers.Out;
using Core.Common.Utility;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace App.Test.UnitTests.App.Service.Transformers
{
    [TestClass]
    public class WsShopTransformerTest : AutoMockFixture<WsShopTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            var exclusion = new List<string> { "ShopNews" };
            var input = fixture.Create<ShopInfo>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            new WsShopNewsTransformer().Initialise();
            var act = transformer.Transform<ShopInfo, WsShop>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act, exclusion));

        }
    }
}
