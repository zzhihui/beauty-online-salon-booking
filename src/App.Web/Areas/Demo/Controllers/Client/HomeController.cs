using System.Web.Mvc;
using App.Web.Controllers;

namespace App.Web.Areas.Demo.Controllers.Client
{
    public class HomeController : BaseController
    {
        // GET: AsianShop/Home
        public ActionResult Index()
        {
            return View();
        }
    
        public ActionResult Home()
        {
            return PartialView();
        }

        public ActionResult Gallery()
        {
            return PartialView();
        }

        public ActionResult ContactUs()
        {
            return PartialView();
        }

        public ActionResult Help()
        {
            return PartialView();
        }

        public ActionResult About()
        {
            return PartialView();
        }

        public ActionResult Booking()
        {
            return PartialView();
        }

        public ActionResult Hair()
        {
            return PartialView();
        }

        public ActionResult Nail()
        {
            return PartialView();
        }
    }
}