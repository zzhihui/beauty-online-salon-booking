using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Shop;
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
    public class WsPaymentTypeTransformerTest : AutoMockFixture<WsPaymentTypeTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            ;
            var input = fixture.Create<PaymentType>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            var act = transformer.Transform<PaymentType, WsPaymentType>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act));

        }
    }
}
