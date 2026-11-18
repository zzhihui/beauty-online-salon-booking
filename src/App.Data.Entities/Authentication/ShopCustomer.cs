using System;
using System.Collections.Generic;
using App.Data.Entities.Shops;

namespace App.Data.Entities.Authentication
{
    public class ShopCustomer : BaseDataEntity
    {
        public string ContactName => FirstName + " " + LastName;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StoreCredit { get; set; }

        public string PhoneNumber { get; set; }

        public string OldPassword { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public int? Ranking { get; set; }

        public string Username { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? TokenExpire { get; set; }

        public string ClientToken { get; set; }

        public List<Booking> BookingHistory { get; set; }
    }
}