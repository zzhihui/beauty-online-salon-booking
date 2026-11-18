using System.Collections.Generic;

namespace App.Service.Contract.Entities.Shop
{
    public class WsShop : WsEntity
    {
        public string Name { get; set; }
        public string ShopNumber { get; set; }
        public string ShopMobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
       public string Email { get; set; }
        public decimal? MinOrder { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public bool? TakePayment { get; set; }
        public string GoogleMap { get; set; }
        public string IoSlink { get; set; }
        public string Androidlink { get; set; }
        public bool? IsOpen { get; set; }
        public string ShopOpenHours { get; set; }
        public string ShopDeliveryOpenHours { get; set; }
        public bool? Isactive { get; set; }
        public List<WsShopNews> ShopNews { get; set; }
        public WsShop()
        {
            ShopNews = new List<WsShopNews>();  
        }
    }
}