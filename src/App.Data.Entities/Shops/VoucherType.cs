namespace App.Data.Entities.Shops
{
    public class VoucherType : BaseDataEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int ServiceId { get; set; }
    }
}
