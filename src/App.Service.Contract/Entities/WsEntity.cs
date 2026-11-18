using System;

namespace App.Service.Contract.Entities
{
    public class WsEntity 
    {
        public int Id { get; set; }
        public int? ShopId { get; set; }
        
        public string Guid { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}