using System.Collections.Generic;
using App.Data.Entities.Payment;
using App.Service.Contract.Entities.Payment;
using App.Service.Transformers;
using App.Service.Transformers.In;
using Core.Common.Utility;
using Core.Test.Utilities.Fixtures;
using Core.Test.Utilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace App.Test.UnitTests.App.Service.Transformers
{
    [TestClass]
    public class PaymentTransformerTest : AutoMockFixture<PaymentTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            var input = fixture.Create<WsPayment>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            var act = transformer.Transform<WsPayment, Payment>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act));

        }
    }
}
