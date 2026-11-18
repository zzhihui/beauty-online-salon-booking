using System.Collections.Generic;
using App.Service.Contract.Entities.Authentication;
using App.Service.Contract.Entities.Shop;

namespace App.Web.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        public List<WsShopCustomer> Customers { get; set; }

        public List<WsVoucherType> VoucherTypes { get; set; } 

        public WsShopCustomer CurrentCustomer { get; set; }
    }
}