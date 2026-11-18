using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.BusinessEngine.MangoDb;
using App.Data.Entities.Shops;
using Core.Common.Infrastructure.Autofac;

namespace App.BusinessEngine.BusinessEngine
{
    public interface ISchedulerBusinessEngine
    {
        List<DateTime> GetAvailableSlotForDay(DateTime bookingDay, int serviceId, int shopId,
            Dictionary<int, List<ShopUserScheduler>> existingSchedules);
    }

    [BusinessEngine]
    public class SchedulerBusinessEngine : BusinessEngineBase, ISchedulerBusinessEngine
    {
        private readonly IMangoDbContext _mangoDbContext;

        public SchedulerBusinessEngine(IMangoDbContext mangoDbContext)
        {
            _mangoDbContext = mangoDbContext;
        }

        private const int period = 30;

        //private DateTime _startTime = new DateTime(1900, 1, 1, 9, 0, 0);
        //private DateTime _endTime = new DateTime(1900, 1, 1, 17, 0, 0);

        //private Tuple<DateTime, int> slot1 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 9, 0, 0), 90);
        //private Tuple<DateTime, int> slot2 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 12, 0, 0), 60);
        //private Tuple<DateTime, int> slot3 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 15, 0, 0), 90);

        ////private List<Tuple<DateTime, int>> testBooking = new List<Tuple<DateTime, int>>();

        //private Tuple<DateTime, int> slot4 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 10, 0, 0), 60);
        //private Tuple<DateTime, int> slot5 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 13, 0, 0), 90);
        //private Tuple<DateTime, int> slot6 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 16, 30, 0), 60);

        //private Tuple<DateTime, int> slot7 = new Tuple<DateTime, int>(new DateTime(2019, 7, 30, 13, 0, 0), 60);

        //bookingDay = DateTime.Today;
        //serviceLength = 90;

        public List<DateTime> GetAvailableSlotForDay(DateTime bookingDay, int serviceId, int shopId,
            Dictionary<int, List<ShopUserScheduler>> existingSchedules)
        {
            var service = _mangoDbContext.GetShopServiceById(serviceId, shopId);
            var serviceLength = service.Length;

            var shopSettings = _mangoDbContext.GetShopSettings(shopId);

            //testBooking.AddRange(new List<Tuple<DateTime, int>>() { slot7 });
            //testBooking.AddRange(new List<Tuple<DateTime, int>>() { slot4, slot5, slot6 });

            //var currentDay = DateTime.Today;

            var startTime = new DateTime(bookingDay.Year, bookingDay.Month, bookingDay.Day,
                shopSettings.OpenTime.Hour, shopSettings.OpenTime.Minute, shopSettings.OpenTime.Second);

            var endTime = new DateTime(bookingDay.Year, bookingDay.Month, bookingDay.Day,
                shopSettings.CloseTime.Hour, shopSettings.CloseTime.Minute, shopSettings.CloseTime.Second);

            var availableSlotStartsTime = new List<DateTime>();

            for (var newSlotStartTime = startTime; newSlotStartTime <= endTime.AddMinutes(-serviceLength); newSlotStartTime = newSlotStartTime.AddMinutes(period))
            {
                var isAvailable = true;

                var newSlotEndTime = newSlotStartTime.AddMinutes(serviceLength);

                if (existingSchedules != null && existingSchedules.Any())
                {
                    foreach (var scheduler in existingSchedules)
                    {
                        isAvailable = true;

                        if (scheduler.Value != null && scheduler.Value.Any())
                        {
                            foreach (var shopUserScheduler in scheduler.Value)
                            {
                                var existingSlotStartTime = shopUserScheduler.StartDateTime;
                                var existingSlotEndTime = shopUserScheduler.StartDateTime.AddMinutes(shopUserScheduler.Length);

                                if (newSlotStartTime >= existingSlotStartTime && newSlotStartTime < existingSlotEndTime)
                                {
                                    isAvailable = false;
                                    break;
                                }

                                if (newSlotEndTime > existingSlotStartTime && newSlotEndTime <= existingSlotEndTime)
                                {
                                    isAvailable = false;
                                    break;
                                }

                                if (newSlotStartTime < existingSlotStartTime && newSlotEndTime > existingSlotEndTime)
                                {
                                    isAvailable = false;
                                    break;
                                }
                            }
                        }

                        if (isAvailable)
                            availableSlotStartsTime.Add(newSlotStartTime);
                    }
                }
                else
                {
                    availableSlotStartsTime.Add(newSlotStartTime);
                }
                
            }

            return availableSlotStartsTime.Where(x => x > DateTime.Now).Distinct().ToList();
        }
    }
}
