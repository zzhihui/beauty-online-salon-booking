using System.Collections.Generic;
using App.Data.Entities.Authentication;

namespace App.Data.Entities.Shops
{
    public class ShopService : BaseDataEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Length { get; set; }
        public int MaxPerDay { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public List<ShopUser> ServiceUsers { get; set; } 
    }
}

