namespace App.Data.Entities.Authentication
{
    public class ShopUser : BaseDataEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public int UserGroupId { get; set; }
        public string Pin { get; set; }
        public string DisplayName { get; set; }
    }
}

