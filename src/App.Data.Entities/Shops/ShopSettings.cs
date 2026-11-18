using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities.Shops
{
    public class ShopSettings : BaseDataEntity
    {
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }
}
