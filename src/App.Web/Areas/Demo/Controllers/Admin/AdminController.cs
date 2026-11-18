using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using App.Service.Contract.Entities.Shop;
using App.Web.Application.Filter;
using App.Web.Application.ServiceClient;
using App.Web.Controllers;
using App.Web.ViewModels;

namespace App.Web.Areas.Demo.Controllers.Admin
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
            var result = _shopUserServiceClient.GetAllBookings(ShopId);

            var model = new BookingViewModel {CustomerBookings = result.Content};

            return View(model);
        }

        [AreaAuth]
        public ActionResult Bookings()
        {
            //Get All Bookings From DB
            var result = _shopUserServiceClient.GetAllBookings(ShopId);

            var model = new BookingViewModel { CustomerBookings = result.Content };

            return View(model);
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
            var bookings = _shopUserServiceClient.GetAllSchedules(ShopId).Content;
            //var shopUsers = _shopUserServiceClient.GetAllShopUsers(ShopId).Content;
            //var services = _shopUserServiceClient.GetAllShopServices(ShopId).Content;
            //var customers = _shopUserServiceClient.GetAllCustomers(ShopId).Content;

            var schedules = bookings.Select(booking => new ScheduleViewModel
            {
                ServiceId = booking.ServiceId,
                CustomerId = booking.CustomerId,
                ShopUserId = booking.ShopUserId,
                BookingId = booking.BookingId,
                Length = booking.Length,
                Subject = booking.Subject,
                Description = booking.Description,
                StartDateTime = booking.StartDateTime,
                EndDateTime = booking.StartDateTime.AddMinutes(booking.Length),
                ScheduleId = booking.ScheduleId,
                ShopUserName = booking.ShopUserName,
                ShopId = booking.ShopId,
                Status = booking.Status
                //ServiceName = services.FirstOrDefault(x => x.Id == booking.ServiceId).Name,
                //Ref = booking.Ref,
                //CustomerMobile = customers.FirstOrDefault(x => x.Id == booking.CustomerId) == null ? "" : customers.FirstOrDefault(x => x.Id == booking.CustomerId).MobileNumber,
                //CustomerName = customers.FirstOrDefault(x => x.Id == booking.CustomerId) == null ? "" : customers.FirstOrDefault(x => x.Id == booking.CustomerId).FirstName,
                //ServiceLength = services.FirstOrDefault(x => x.Id == booking.ServiceId).Length,
                //ShopUserName = shopUsers.FirstOrDefault(x => x.Id == booking.ShopUserId).DisplayName
            }).ToList();
            
            return View(schedules);
        }
    }
}