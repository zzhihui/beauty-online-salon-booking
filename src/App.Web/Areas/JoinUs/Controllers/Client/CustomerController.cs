using System.Web.Mvc;
using App.Web.Controllers;

namespace App.Web.Areas.JoinUs.Controllers.Client
{
    public class CustomerController : BaseController
    {
        // GET: AsianShop/Customer
        public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult Register()
        {
            return PartialView();
        }

        public ActionResult MyProfile()
        {
            return PartialView();
        }

        public ActionResult EditProfile()
        {
            return PartialView();
        }

        public ActionResult Logoff()
        {
            return PartialView();
        }
        public ActionResult ForgetPass()
        {
            return PartialView();
        }

        public ActionResult MyBookings()
        {
            return PartialView();
        }

    }
}