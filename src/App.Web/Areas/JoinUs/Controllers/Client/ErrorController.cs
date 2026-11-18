using System.Web.Mvc;
using App.Web.Controllers;

namespace App.Web.Areas.JoinUs.Controllers.Client
{
    public class ErrorController : BaseController
    {
        
        public ActionResult Index()
        {
            return PartialView();
        }


        public ActionResult Error403()
        {
            return PartialView();
        }


        public ActionResult Error405()
        {
            return PartialView();
        }

        
    }
}