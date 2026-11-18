using System.Collections.Generic;
using App.Service.Contract.Entities.Authentication;

namespace App.Service.Contract.Entities.Shop
{
    public class WsShopService : WsEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Length { get; set; }
        public int MaxPerDay { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public List<WsShopUser> ServiceUsers { get; set; } 
    }
}
