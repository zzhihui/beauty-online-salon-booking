using System;
using System.Collections.Generic;
using System.Linq;
using App.BusinessEngine.BusinessEngine;
using App.Data.Entities.Shops;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;
using App.Service.Transformers;
using Core.Common.Core;
using Core.Common.Infrastructure.Autofac;
using Core.Common.Validation;
using FluentValidation;

namespace App.Service.Services
{
    public interface IShopServiceHandler
    {
        WsShop GetShopInfo(int shopId);
        WsShop UpdateShopInfo(WsShop shopInfo, int shopId);
        List<WsShopService> GetAllServices(int shopId);
        bool BookService(WsBooking booking);
        List<string> GetAllServiceCategories(int shopId);
        List<string> GetAvailableSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId);
        List<WsSchedule> GetSchedules(int shopId);
        int SaveAppointment(WsSchedule schedule);
        int DeleteAppointment(WsSchedule schedule);
        int Addppointment(WsSchedule schedule);
    }

    [ServiceHandler]
    public class ShopServiceHandler : ServiceBase, IShopServiceHandler
    {
        private readonly IShopBusinessEngine _shopBusinessEngine;
        private readonly ISchedulerBusinessEngine _schedulerBusinessEngine;

        public ShopServiceHandler(IValidatorFactory validatorFactory, 
            IUnitOfWork unitOfWork, 
            ITransformer transformer, 
            IShopBusinessEngine shopBusinessEngine,
            ISchedulerBusinessEngine schedulerBusinessEngine) 
            : base(validatorFactory, unitOfWork, transformer)
        {
            _shopBusinessEngine = shopBusinessEngine;
            _schedulerBusinessEngine = schedulerBusinessEngine;
        }
        
        public WsShop GetShopInfo(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);

                //Perform
                var shopInfo = _shopBusinessEngine.GetShopInfo(shopId);
                if (shopInfo == null)
                    return null;
                return Transformer.Transform<ShopInfo, WsShop>(shopInfo);

            });
        }

        public List<string> GetAllServiceCategories(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);

                //Perform
                var serviceInfo = _shopBusinessEngine.GetShopService(shopId);
                if (serviceInfo == null)
                    return null;

                //return Transformer.Transform<List<ShopService>, List<WsShopService>>(serviceInfo);
                //return serviceInfo.Select(service => Transformer.Transform<ShopService, WsShopService>(service)).ToList();

                return serviceInfo.Select(x => x.Category).Distinct().ToList();
            });
        }

        public List<string> GetAvailableSlots(int shopId, DateTime startDateTime, int serviceId, int shopUserId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);
                Guard.GreaterThanZero(serviceId);

                var bookedSlots = new Dictionary<int, List<ShopUserScheduler>>();

                //1. With specific shopUserId, get booked slots from DB.
                if (shopUserId > -1)
                {
                    bookedSlots.Add(shopUserId, _shopBusinessEngine.GetBookedSlots(shopId, startDateTime, serviceId, shopUserId));
                }
                else
                {
                    var serviceUsers = _shopBusinessEngine.GetShopServiceUser(shopId).Where(x => x.ServiceId == serviceId).ToList();

                    foreach (var s in serviceUsers)
                    {
                        bookedSlots.Add(s.UserId, _shopBusinessEngine.GetBookedSlots(shopId, startDateTime, serviceId, s.UserId));
                    }
                }

                //2. Without shopUserId (-1), 

                
                //3. Call calculation engine to get back available time slots.
                var availableSlots =
                    _schedulerBusinessEngine.GetAvailableSlotForDay(startDateTime, serviceId, shopId, bookedSlots);

                var result = new List<string>();

                foreach (var availableSlot in availableSlots)
                {
                    //var time = availableSlot.Hour.ToString("D2") 
                    //           + ":" + availableSlot.Minute.ToString("D2") 
                    //           + (availableSlot.Hour > 12 ? " PM" : " AM");

                    var time = availableSlot.ToString("hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
                        .ToUpper();

                    result.Add(time);
                }

                //if (availableSlots == null)
                //    return null;

                //return Transformer.Transform<List<ShopService>, List<WsShopService>>(serviceInfo);
                //return serviceInfo.Select(service => Transformer.Transform<ShopService, WsShopService>(service)).ToList();

                return result;
            });
        }

        public List<WsSchedule> GetSchedules(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);

                var startDateTime = DateTime.Now.AddDays(-30);
                var endDateTime = DateTime.Now.AddDays(90);

                var bookedSlots = _shopBusinessEngine.GetBookedSlotsForScheduler(shopId, startDateTime, endDateTime);
                //var customers = _shopBusinessEngine.GetAllCustomers(shopId);
                var shopUsers = _shopBusinessEngine.GetAllShopUser(shopId);
                //var services = _shopBusinessEngine.GetAllShopService(shopId);

                var schedules = bookedSlots.Select(slot => new WsSchedule
                {
                    ShopId = slot.ShopId,
                    ServiceId = slot.ServiceId,
                    ShopUserId = slot.ShopUserId,
                    ShopUserName = shopUsers.FirstOrDefault(x => x.Id == slot.ShopUserId)?.DisplayName,
                    BookingId = slot.BookingId,
                    Length = slot.Length,
                    Guid = slot.BookingId,
                    Description = slot.Description,
                    CustomerId = slot.CustomerId,
                    Subject = slot.Subject,
                    Status = slot.Status,
                    StartDateTime = slot.StartDateTime,
                    ScheduleId = slot.ScheduleId
                }).ToList();

                foreach (var shopUser in shopUsers)
                {
                    if (schedules.All(x => x.ShopUserId != shopUser.Id))
                    {
                        schedules.Add(new WsSchedule
                        {
                            ShopId = shopId,
                            ServiceId = 0,
                            ShopUserId = shopUser.Id,
                            ShopUserName = shopUser.DisplayName,
                            BookingId = "0",
                            Length = 0,
                            Guid = Guid.NewGuid().ToString(),
                            Description = "Out of Office",
                            CustomerId = 0,
                            Status = "Out of Office",
                            Subject = "Out of Office",
                            StartDateTime = DateTime.Today,
                            ScheduleId = Guid.NewGuid().ToString()
                        });
                    }
                }

                return schedules;

                ////Perform
                //var serviceInfo = _shopBusinessEngine.GetShopService(shopId);
                //if (serviceInfo == null)
                //    return null;

                ////return Transformer.Transform<List<ShopService>, List<WsShopService>>(serviceInfo);
                ////return serviceInfo.Select(service => Transformer.Transform<ShopService, WsShopService>(service)).ToList();

                //return serviceInfo.Select(service => new WsShopService
                //{
                //    ShopId = service.ShopId,
                //    Id = service.Id,
                //    Name = service.Name,
                //    Category = service.Category,
                //    Description = service.Description,
                //    Length = service.Length,
                //    MaxPerDay = service.MaxPerDay,
                //    PriceFrom = service.PriceFrom,
                //    PriceTo = service.PriceTo,
                //    ServiceUsers = service.ServiceUsers.Select(serviceUser => new WsShopUser
                //    {
                //        DisplayName = serviceUser.DisplayName,
                //        Id = serviceUser.Id
                //    }).ToList()
                //}).ToList();
            });
        }

        public int SaveAppointment(WsSchedule schedule)
        {
            return Query(() =>
            {
                var result = _shopBusinessEngine.SaveSchedule(schedule);

                return result;
            });
        }

        public int DeleteAppointment(WsSchedule schedule)
        {
            return Query(() =>
            {
                var result = _shopBusinessEngine.DeleteSchedule(schedule);

                return result;
            });
        }

        public int Addppointment(WsSchedule schedule)
        {
            return Query(() =>
            {
                var result = _shopBusinessEngine.AddSchedule(schedule);

                return result;
            });
        }

        public List<WsShopService> GetAllServices(int shopId)
        {
            return Query(() =>
            {
                Guard.GreaterThanZero(shopId);

                //Perform
                var serviceInfo = _shopBusinessEngine.GetShopService(shopId);
                if (serviceInfo == null)
                    return null;

                //return Transformer.Transform<List<ShopService>, List<WsShopService>>(serviceInfo);
                //return serviceInfo.Select(service => Transformer.Transform<ShopService, WsShopService>(service)).ToList();

                return serviceInfo.Select(service => new WsShopService
                {
                    ShopId = service.ShopId,
                    Id = service.Id,
                    Name = service.Name,
                    Category = service.Category,
                    Description = service.Description,
                    Length = service.Length,
                    MaxPerDay = service.MaxPerDay,
                    PriceFrom = service.PriceFrom,
                    PriceTo = service.PriceTo,
                    ServiceUsers = service.ServiceUsers.Select(serviceUser => new WsShopUser
                    {
                        DisplayName = serviceUser.DisplayName,
                        Id = serviceUser.Id
                    }).ToList()
                }).ToList();
            });
        }

        public bool BookService(WsBooking booking)
        {
            return Query(() =>
            {
                var requestTransIn = Transformer.Transform<WsBooking, Booking>(booking);

                var bookedSlots = new Dictionary<int, List<ShopUserScheduler>>();

                var availableUsers = new List<int>();

                var valid = false;
                //1. if no user, set a user.
                if (requestTransIn.ShopUserId <= 0)
                {
                    var serviceUsers = _shopBusinessEngine.GetShopServiceUser(requestTransIn.ShopId)
                        .Where(x => x.ServiceId == requestTransIn.ServiceId).ToList();

                    foreach (var s in serviceUsers)
                    {
                        bookedSlots.Clear();

                        bookedSlots.Add(s.UserId, _shopBusinessEngine.GetBookedSlots(requestTransIn.ShopId, requestTransIn.DateTimeSlot,
                            requestTransIn.ServiceId, s.UserId));

                        var availableSlots =
                            _schedulerBusinessEngine.GetAvailableSlotForDay(requestTransIn.DateTimeSlot, requestTransIn.ServiceId,
                                requestTransIn.ShopId, bookedSlots);

                        if (availableSlots.Contains(requestTransIn.DateTimeSlot))
                        {
                            availableUsers.Add(s.UserId);
                            valid = true;
                        }
                    }

                    if (availableUsers.Count > 1)
                    {
                        var userId = availableUsers[0];
                        var existingBookings = 100;
                        foreach (var availableUser in availableUsers)
                        {
                            var bookings = _shopBusinessEngine.GetAllBookingsForShopUserOnDate(requestTransIn.ShopId,
                                availableUser, booking.DateTimeSlot);

                            if (bookings == null)
                            {
                                userId = availableUser;
                                break;
                            }

                            if(bookings.Count < existingBookings)
                            {
                                userId = availableUser;
                                existingBookings = bookings.Count;
                            }
                        }

                        requestTransIn.ShopUserId = userId;
                    }
                    else
                    {
                        requestTransIn.ShopUserId = availableUsers[0];
                    }
                }
                else
                {
                    //validate as well.
                    bookedSlots.Add(requestTransIn.ShopUserId, 
                        _shopBusinessEngine.GetBookedSlots(requestTransIn.ShopId, requestTransIn.DateTimeSlot, 
                            requestTransIn.ServiceId, requestTransIn.ShopUserId));

                    var availableSlots =
                        _schedulerBusinessEngine.GetAvailableSlotForDay(requestTransIn.DateTimeSlot, requestTransIn.ServiceId,
                            requestTransIn.ShopId, bookedSlots);

                    if (availableSlots.Contains(requestTransIn.DateTimeSlot))
                        valid = true;
                }

                if (!valid)
                    throw new System.Exception("It looks like the service has been booked, please try a different time or stylist.");

                //2. update booking table.
                var bookingResult = _shopBusinessEngine.BookService(requestTransIn);

                requestTransIn.BookingId = bookingResult;
                //3. update schedule table.
                var scheduleResult = _shopBusinessEngine.UpdateScheduler(requestTransIn);

                if (!scheduleResult)
                {
                    throw new System.Exception("It looks like the service has been booked, please try a different time or stylist.");
                }

                //Perform
                return true;
            });
        }

        public WsShop UpdateShopInfo(WsShop shopInfo, int shopId)
        {
            throw new NotImplementedException();
        }
    }
}