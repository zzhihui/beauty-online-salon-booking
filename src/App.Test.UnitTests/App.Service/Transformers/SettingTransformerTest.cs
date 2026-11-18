using App.Data.Entities.Settings;
using App.Service.Contract.Entities.Settings;
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
    public class SettingTransformerTest : AutoMockFixture<SettingTransformer>
    {
        [TestMethod]
        public void MappingTest()
        {

            var fixture = new Fixture().Customize(new DoNotFillCollectionProperties());
            ;
            var input = fixture.Create<WsSetting>();
            var transformer = new TransformerBase();
            Sut.Initialise();
            var act = transformer.Transform<WsSetting, ServerSetting>(input);
            Assert.IsTrue(ObjectsCompareUtil.IsEqual(input, act));

        }
    }
}
