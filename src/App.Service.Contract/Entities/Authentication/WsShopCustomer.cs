using System;
using System.Collections.Generic;
using App.Service.Contract.Entities.Shop;

namespace App.Service.Contract.Entities.Authentication
{
    public class WsShopCustomer : WsEntity
    {
        public string ContactName { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int StoreCredit { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
        public int? Ranking { get; set; }

        public bool? IsActive { get; set; }

        public string ClientToken { get; set; }

        public DateTime? TokenExpire { get; set; }

        public List<WsBooking> BookingHistory { get; set; }
    }
}