using System;

namespace App.Service.Contract.Entities.Shop
{
    public class WsBooking : WsEntity
    {
        public string Ref { get; set; }
        public DateTime DateTimeSlot { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int ShopUserId { get; set; }
        public string Status { get; set; }
        public string ServiceName { get; set; }
    }
}
