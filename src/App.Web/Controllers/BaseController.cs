using System.Web.Mvc;
using App.Web.Application.Utility;

namespace App.Web.Controllers
{
    public class BaseController: Controller
    {
        protected const string ServiceUrl = "https://beautyfairy.ie";
        protected const string ServiceUrlLocal = "http://localhost:49041";
        protected const int ShopIdDemo = 2;

        protected static int ShopId;

        public BaseController()
        {

#if DEBUG
            Constants.SiteUrl = ServiceUrlLocal;
#else
            Constants.SiteUrl = ServiceUrl;
#endif
        }
    }
}