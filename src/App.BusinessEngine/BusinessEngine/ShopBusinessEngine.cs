using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using App.BusinessEngine.MangoDb;
using App.BusinessEngine.Utility;
using App.Data.Entities.Authentication;
using App.Data.Entities.Enum;
using App.Data.Entities.Shops;
using App.Data.Repositories;
using App.Service.Contract.Entities.Shop;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Utility;

namespace App.BusinessEngine.BusinessEngine
{
    public interface IShopBusinessEngine : IBusinessEngine<IShopBusinessEngine>
    {
        ShopInfo GetShopInfo(int shopId);
        List<ShopService> GetShopService(int shopId);

        List<ShopServiceUser> GetShopServiceUser(int shopId);

        ShopUser GetShopUserById(int id, int shopId);

        int DeleteShopUser(int id, int shopId);
        int SaveShopUser(ShopUser shopUser, int shopId);

        List<ShopUser> GetAllShopUser(int shopId);

        string BookService(Booking request);
        ShopUser LoginShopAgent(string username, string password, int shopId);
        List<Booking> GetAllBookings(int shopId);
        List<ShopCustomer> GetAllCustomers(int shopId);
        string GetServiceName(int serviceId);
        string UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus);
        string UpdateUserCredit(int customerId, int shopId, int credits);
        string SendUserVoucher(int customerId, int shopId, int voucherId);
        List<ContactForm> GetUserFeedback(int shopId);
        List<VoucherType> GetAllVoucherTypes(int shopId);

        List<ShopService> GetAllShopService(int shopId);

        ShopCustomer GetCustomerById(int customerId, int shopId);

        VoucherType GetVoucherTypeById(int id, int shopId);
        ShopService GetShopServiceById(int id, int shopId);

        int SaveCustomer(ShopCustomer customer, int shopId);

        int SaveVoucherType(VoucherType voucher, int shopId);

        int SaveShopService(ShopService shopService, int shopId);

        int DeleteVoucherType(int id, int shopId);

        int DeleteShopService(int id, int shopId);
        List<ShopUserScheduler> GetBookedSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId);

        List<Booking> GetAllBookingsForShopUserOnDate(int shopId, int shopUserId, DateTime scheduledDate);
        bool UpdateScheduler(Booking requestTransIn);

        List<ShopUserScheduler> GetBookedSlotsForScheduler(int shopId, DateTime startDateTime, DateTime endDateTime);
        int SaveSchedule(WsSchedule schedule);
        int DeleteSchedule(WsSchedule schedule);
        int AddSchedule(WsSchedule schedule);
    }

    [BusinessEngine]
    public class ShopBusinessEngine : BusinessEngineBase, IShopBusinessEngine
    {
        private readonly IShopInfoRepository _shopInfoRepository;
        //private readonly IShopNewsRepository _shopNewsRepository;
        private readonly IMangoDbContext _mangoDbContext;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IEmailSender _emailSender;

        public ShopBusinessEngine(IShopInfoRepository shopInfoRepository, 
            IMangoDbContext mangoDbContext, 
            IEncryptionHelper encryptionHelper, 
            IEmailSender emailSender)
        {
            _shopInfoRepository = shopInfoRepository;
            _mangoDbContext = mangoDbContext;
            _encryptionHelper = encryptionHelper;
            _emailSender = emailSender;
        }

        public int DeleteShopService(int id, int shopId)
        {
            return Execute(() => _mangoDbContext.DeleteShopService(id, shopId));
        }

        public List<ShopUserScheduler> GetBookedSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId)
        {
            return Execute(() => _mangoDbContext.GetBookedSlots(shopId, startDateTime, serviceId, shopUserId));
        }

        public List<ShopUserScheduler> GetBookedSlotsForScheduler(int shopId, DateTime startDateTime, DateTime endDateTime)
        {
            return Execute(() =>
            {
                var schedules = _mangoDbContext.GetAllBookedSlotsForScheduler(shopId, startDateTime, endDateTime);

                return schedules ?? new List<ShopUserScheduler>();
            });
        }

        public int SaveSchedule(WsSchedule schedule)
        {
            return Execute(() => _mangoDbContext.SaveSchedule(schedule));
        }

        public int DeleteSchedule(WsSchedule schedule)
        {
            return Execute(() => _mangoDbContext.DeleteSchedule(schedule));
        }

        public int AddSchedule(WsSchedule schedule)
        {
            return Execute(() => _mangoDbContext.AddShopSchedule(schedule));
        }

        public int DeleteShopUser(int id, int shopId)
        {
            return Execute(() => _mangoDbContext.DeleteShopUser(id, shopId));
        }

        public int DeleteVoucherType(int id, int shopId)
        {
            return Execute(() => _mangoDbContext.DeleteVoucherType(id, shopId));
        }

        public ShopService GetShopServiceById(int id, int shopId)
        {
            return Execute(() => _mangoDbContext.GetShopServiceById(id, shopId));
        }

        public ShopUser GetShopUserById(int id, int shopId)
        {
            return Execute(() => _mangoDbContext.GetShopUserById(id, shopId));
        }

        public VoucherType GetVoucherTypeById(int id, int shopId)
        {
            return Execute(() => _mangoDbContext.GetVoucherTypeById(id, shopId));
        }

        public int SaveCustomer(ShopCustomer customer, int shopId)
        {
            return Execute(() => _mangoDbContext.SaveCustomer(customer, shopId));
        }

        public int SaveShopUser(ShopUser shopUser, int shopId)
        {
            return Execute(() => _mangoDbContext.SaveOrUpdateShopUser(shopUser, shopId));
        }

        public int SaveVoucherType(VoucherType voucher, int shopId)
        {
            return Execute(() => _mangoDbContext.SaveOrUpdateVoucherType(voucher, shopId));
        }

        public int SaveShopService(ShopService shopService, int shopId)
        {
            return Execute(() => _mangoDbContext.SaveOrUpdateShopService(shopService, shopId));
        }

        public ShopCustomer GetCustomerById(int customerId, int shopId)
        {
            return Execute(() => _mangoDbContext.GetCustomerById(customerId, shopId));
        }

        public List<ContactForm> GetUserFeedback(int shopId)
        {
            return Execute(() => _mangoDbContext.GetUserFeedback(shopId));
        }

        public List<ShopService> GetAllShopService(int shopId)
        {
            return Execute(() => _mangoDbContext.GetShopService(shopId));
        }

        public List<VoucherType> GetAllVoucherTypes(int shopId)
        {
            return Execute(() => _mangoDbContext.GetAllVoucherTypes(shopId));
        }

        public string SendUserVoucher(int customerId, int shopId, int voucherId)
        {
            return Execute(() =>
            {
                var voucherRef = _mangoDbContext.SendUserVoucher(customerId, shopId, voucherId);

                var email = _mangoDbContext.GetCustomerById(customerId, shopId).Email;
                
                //Send email
                return _emailSender.SendPlainEmail(email,
                    ConfigurationManager.AppSettings["SendVoucherSubject"], $"Your new voucher is {voucherRef}") ? "Voucher Sent" : "Voucher Not Sent";
            });
        }

        public string UpdateUserCredit(int customerId, int shopId, int credits)
        {
            return Execute(() =>
            {
                var newCredits = _mangoDbContext.UpdateUserCredit(customerId, shopId, credits);

                //TODO: Send Email/ SMS Notification
                
                return newCredits;
            });
        }

        public string UpdateUserBookingStatus(int bookingId, int shopId, int bookingStatus)
        {
            return Execute(() =>
            {
                var newBookingStatus = _mangoDbContext.UpdateUserBookingStatus(bookingId, shopId, bookingStatus);

                //TODO: Send Email/ SMS Notification
                if (bookingStatus == (int)BookingStatusEnum.Confirmed)
                {

                }
                else if (bookingStatus == (int)BookingStatusEnum.Rejected)
                {
                }

                return newBookingStatus;
            });
        }

        public List<Booking> GetAllBookings(int shopId)
        {
            return Execute(() => _mangoDbContext.GetAllBookingsByShopId(shopId));
        }

        public List<Booking> GetAllBookingsForShopUserOnDate(int shopId, int shopUserId, DateTime scheduledDate)
        {
            return Execute(() => _mangoDbContext.GetAllBookingsForShopUserOnDate(shopId, shopUserId, scheduledDate));
        }

        public List<ShopCustomer> GetAllCustomers(int shopId)
        {
            return Execute(() => _mangoDbContext.GetAllCustomersByShopId(shopId));
        }

        public string GetServiceName(int serviceId)
        {
            return Execute(() => _mangoDbContext.GetServiceName(serviceId));
        }

        public List<ShopUser> GetAllShopUser(int shopId)
        {
            return Execute(() => _mangoDbContext.GetAllShopUser(shopId));
        }

        public List<ShopServiceUser> GetShopServiceUser(int shopId)
        {
            return Execute(() => _mangoDbContext.GetShopServiceUser(shopId));
        }

        public List<ShopService> GetShopService(int shopId)
        {
            //TODO: DB
            return Execute(() => _mangoDbContext.GetShopService(shopId));
        }

        public ShopUser LoginShopAgent(string username, string password, int shopId)
        {
            return Execute(() =>
            {
                password = _encryptionHelper.EncryptXml(password);
                return _mangoDbContext.LoginShopAgent(username, password, shopId);
            });
        }

        public string BookService(Booking request)
        {
            //TODO: DB
            return Execute(() =>
            {
                //Get current new and confirmed booking for customer.
                var currentBookings = _mangoDbContext.GetAllBookingsByCustomerId(request.CustomerId, request.ShopId);
                var customer = _mangoDbContext.GetCustomerById(request.CustomerId, request.ShopId);

                //_mangoDbContext.DeleteCustomerBookings(customer.Id, customer.ShopId);

                //if (currentBookings != null)
                //{
                //    foreach (var booking in currentBookings)
                //    {
                //        var test = DateTime.Today;

                //        if (booking.DateTimeSlot.Date < DateTime.Now.Date 
                //            && (booking.Status != BookingStatusEnum.Attended.ToString()
                //                || booking.Status != BookingStatusEnum.Rejected.ToString()
                //                || booking.Status != BookingStatusEnum.Cancelled.ToString()))
                //        {
                //            _mangoDbContext.UpdateUserBookingStatus(booking.Id, booking.ShopId, (int) BookingStatusEnum.Expired);
                //        }
                //    }

                //    var activeBooking = currentBookings.Where(x => x.Status == "New" || x.Status == "Confirmed");
                    
                //    //Check total booking.
                //    if (activeBooking.Count() > 5)
                //        throw new System.Exception("You cannot have more than 5 active bookings");

                //    var sameDayBooking = currentBookings.Where(x => (x.Status == "New" || x.Status == "Confirmed") && x.DateTimeSlot.Date == request.DateTimeSlot.Date);
                //    //Check booking for selected date.
                //    if(sameDayBooking.Any())
                //        throw new System.Exception("You already have booking on that day");
                //}
                
                //If no booking on selected date and totally less than 5 bookings, book the service.
                var result = _mangoDbContext.BookService(request);

                if (!string.IsNullOrEmpty(result))
                {
                    _emailSender.SendPlainEmail(customer.Email,
                        ConfigurationManager.AppSettings["InstantBookingResponse"], $"Hi {customer.FirstName},\n \nYour booking request has been sent to the shop, they will get back to you soon.\n \nBeautyFairy");
                }

                return result;
            });
        }

        public bool UpdateScheduler(Booking requestTransIn)
        {
            return _mangoDbContext.AddSchedule(requestTransIn);
        }


        public ShopInfo GetShopInfo(int shopId)
        {
            return Execute(() =>
            {
                var item = _shopInfoRepository.GetShopInfo(shopId);
                return item;
            });
        }
    }
}
