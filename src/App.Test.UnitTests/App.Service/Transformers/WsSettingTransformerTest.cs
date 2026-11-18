using App.Data.Entities.Settings;
using App.Service.Contract.Entities.Settings;
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
    public class WsSettingTransformerTest : AutoMockFixture<WsSettingTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            ;
            var input = fixture.Create<ServerSetting>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            var act = transformer.Transform<ServerSetting, WsSetting>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act));

        }
    }
}
