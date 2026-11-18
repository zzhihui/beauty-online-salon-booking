using System.Collections.Generic;
using App.Service.Contract.Entities.Shop;

namespace App.Web.ViewModels
{
    public class BookingViewModel : BaseViewModel
    {
        public List<WsBooking> CustomerBookings { get; set; }
    }
}