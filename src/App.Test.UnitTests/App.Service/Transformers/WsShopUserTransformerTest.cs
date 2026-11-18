using App.Data.Entities.Authentication;
using App.Service.Contract.Entities.Authentication;
using App.Service.Transformers;
using App.Service.Transformers.Out;
using Core.Common.Utility;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace App.Test.UnitTests.App.Service.Transformers
{
    [TestClass]
    public class WsShopUserTransformerTest : AutoMockFixture<WsShopUserTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            ;
            var input = fixture.Create<ShopUser>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            var act = transformer.Transform<ShopUser, WsShopUser>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act));

        }
    }
}
