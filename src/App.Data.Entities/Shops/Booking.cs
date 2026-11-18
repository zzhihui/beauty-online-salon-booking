using System;

namespace App.Data.Entities.Shops
{
    public class Booking: BaseDataEntity
    {
        public string Ref { get; set; }
        public DateTime DateTimeSlot { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int ShopUserId { get; set; }
        public string Status { get; set; }
        public string BookingId { get; set; }
    }
}
