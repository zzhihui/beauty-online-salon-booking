using System.Collections.Generic;

namespace App.Service.Contract.Entities.Shop
{
    public class WsOrder : WsEntity
    {
        public List<WsMenuItem> OrderItems { get; set; }
        public string DiscountRules { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal OrderOriginalTotal { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public int? CustomerId { get; set; }
        public int? DeliveryOptionId { get; set; }
        public bool? IsInternalOrder { get; set; }
        public int? StatusId { get; set; }
        public int? OrderSource { get; set; }
        public string DriverName { get; set; }
        public WsDeliveryOption DeliveryType { get; set; }
        public decimal DeliveryCharge { get; set; }
        public string PaymentRef { get; set; }
        public string OrderRef { get; set; }
        public string PaymentType { get; set; }
        public int AgentId { get; set; }
        public string CancellationReason { get; set; }
        public WsOrder()
        {
            OrderItems = new List<WsMenuItem>();
        }
    }
}