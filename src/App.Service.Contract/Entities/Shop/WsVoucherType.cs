namespace App.Service.Contract.Entities.Shop
{
    public class WsVoucherType : WsEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int ServiceId { get; set; }
    }
}
