using System;

namespace App.Service.Contract.Entities.Shop
{
    public class WsShopHour : WsEntity
    {
        public string DayOfWeek { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? FinishTime { get; set; }
    }
}