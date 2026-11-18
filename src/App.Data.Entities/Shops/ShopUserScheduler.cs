using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Shops
{
    public class ShopUserScheduler : BaseDataEntity
    {
        public string Status { get; set; }
        public string Subject { get; set; }

        public string Description { get; set; }

        public int Length { get; set; }

        public DateTime StartDateTime { get; set; }

        public string BookingId { get; set; }

        public int ServiceId { get; set; }

        public int CustomerId { get; set; }

        public int ShopUserId { get; set; }

        public string ScheduleId { get; set; }
    }
}
