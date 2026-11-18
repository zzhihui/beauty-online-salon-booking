using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;
using App.Web.ViewModels;

namespace App.Web.Areas.JoinUs.Controllers.Admin
{
    public class AdminController : BaseController
    {
        private readonly IShopUserServiceClient _shopUserServiceClient;

        public AdminController()
        {
            ShopId = ShopIdDemo;
            _shopUserServiceClient= new ShopUserServiceClient();
        }
        
        public ActionResult Login()
        {
            var viewModel = new AdminLoginViewModel();
            return View(viewModel);
        }
       
        public virtual ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminLoginViewModel viewModel)
        {
            var result = _shopUserServiceClient.LoginShopAgent(viewModel.Email, viewModel.Password, ShopId);

            if (!result.IsSucceed)
            {
                viewModel.ErrorMessage = "Invalid email or password!";
                ModelState.AddModelError("", viewModel.ErrorMessage);

                return View(viewModel);
            }

            FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
            return RedirectToAction("Index");
        }

        [AreaAuth]
        public ActionResult Index()
        {
            //Get All Bookings From DB
            //var bookings = _shopUserServiceClient.GetAllBookings(ShopId).Content;
            //var shopUsers = _shopUserServiceClient.GetAllShopUsers(ShopId).Content;
            //var services = _shopUserServiceClient.GetAllShopServices(ShopId).Content;
            //var customers = _shopUserServiceClient.GetAllCustomers(ShopId).Content;

            //var result = bookings.Select(booking => new BookingViewModel
            //{
            //    Created = booking.Created ?? DateTime.Now,
            //    Id = booking.Id,
            //    ServiceId = booking.ServiceId,
            //    DateTimeSlot = booking.DateTimeSlot,
            //    Status = booking.Status,
            //    CustomerId = booking.CustomerId,
            //    ShopUserId = booking.ShopUserId,
            //    ServiceName = services.FirstOrDefault(x => x.Id == booking.ServiceId).Name,
            //    Ref = booking.Ref,
            //    CustomerMobile = customers.FirstOrDefault(x => x.Id == booking.CustomerId).MobileNumber,
            //    CustomerName = customers.FirstOrDefault(x => x.Id == booking.CustomerId).FirstName,
            //    ServiceLength = services.FirstOrDefault(x => x.Id == booking.ServiceId).Length,
            //    ShopUserName = shopUsers.FirstOrDefault(x => x.Id == booking.ShopUserId).DisplayName
            //}).ToList();

            return View();
        }

        [AreaAuth]
        public ActionResult Bookings()
        {
            ////Get All Bookings From DB
            //var bookings = _shopUserServiceClient.GetAllBookings(ShopId).Content;
            //var shopUsers = _shopUserServiceClient.GetAllShopUsers(ShopId).Content;
            //var services = _shopUserServiceClient.GetAllShopServices(ShopId).Content;
            //var customers = _shopUserServiceClient.GetAllCustomers(ShopId).Content;

            //var result = bookings.Select(booking => new BookingViewModel
            //{
            //    Created = booking.Created ?? DateTime.Now,
            //    Id = booking.Id,
            //    ServiceId = booking.ServiceId,
            //    DateTimeSlot = booking.DateTimeSlot,
            //    Status = booking.Status,
            //    CustomerId = booking.CustomerId,
            //    ShopUserId = booking.ShopUserId,
            //    ServiceName = services.FirstOrDefault(x => x.Id == booking.ServiceId).Name,
            //    Ref = booking.Ref,
            //    CustomerMobile = customers.FirstOrDefault(x => x.Id == booking.CustomerId).MobileNumber,
            //    CustomerName = customers.FirstOrDefault(x => x.Id == booking.CustomerId).FirstName,
            //    ServiceLength = services.FirstOrDefault(x => x.Id == booking.ServiceId).Length,
            //    ShopUserName = shopUsers.FirstOrDefault(x => x.Id == booking.ShopUserId).DisplayName
            //}).ToList();

            return View();
        }

        

        [AreaAuth]
        [HttpGet]
        public ActionResult ViewUserFeedback()
        {
            var result = _shopUserServiceClient.GetUserFeedback(ShopId);

            //return new JsonResult { Data = result };
            return View(new CustomerFeedbackViewModel { Feedbacks = result.Content });
        }

        [AreaAuth]
        [HttpGet]
        public ActionResult Scheduler()
        {
            //Get All Bookings From DB
            //var bookings = _shopUserServiceClient.GetAllBookings(ShopId).Content;
            //var shopUsers = _shopUserServiceClient.GetAllShopUsers(ShopId).Content;
            //var services = _shopUserServiceClient.GetAllShopServices(ShopId).Content;
            //var customers = _shopUserServiceClient.GetAllCustomers(ShopId).Content;

            //var result = bookings.Select(booking => new BookingViewModel
            //{
            //    Created = booking.Created ?? DateTime.Now,
            //    Id = booking.Id,
            //    ServiceId = booking.ServiceId,
            //    DateTimeSlot = booking.DateTimeSlot,
            //    Status = booking.Status,
            //    CustomerId = booking.CustomerId,
            //    ShopUserId = booking.ShopUserId,
            //    ServiceName = services.FirstOrDefault(x => x.Id == booking.ServiceId).Name,
            //    Ref = booking.Ref,
            //    CustomerMobile = customers.FirstOrDefault(x => x.Id == booking.CustomerId).MobileNumber,
            //    CustomerName = customers.FirstOrDefault(x => x.Id == booking.CustomerId).FirstName,
            //    ServiceLength = services.FirstOrDefault(x => x.Id == booking.ServiceId).Length,
            //    ShopUserName = shopUsers.FirstOrDefault(x => x.Id == booking.ShopUserId).DisplayName
            //}).ToList();

            return View();
        }
    }
}