using System.Collections.Generic;
using App.Data.Entities.Payment;
using App.Service.Contract.Entities.Payment;
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
    public class WsPaymentInfoTransformerTest : AutoMockFixture<WsPaymentInfoTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            var exclusion = new List<string> { "IsSuccess" };
            var input = fixture.Create<PaymentInfo>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            var act = transformer.Transform<PaymentInfo, WsPaymentInfo>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act, exclusion));

        }
    }
}
