using App.Web.Application.Utility;

namespace App.Test.IntegrationTest.Services
{
    public class BaseServiceTest
    {

        protected int ShopId;
        protected string SiteUrl;

        public BaseServiceTest()
        {
            ShopId = 2;
            SiteUrl = "http://localhost:49041";
        }
    }
}