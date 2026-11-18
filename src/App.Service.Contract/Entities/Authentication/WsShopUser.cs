namespace App.Service.Contract.Entities.Authentication
{
    public class WsShopUser : WsEntity
    {
        /// <summary>
        /// User name for the login user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password for the login user. Note: this is only used for validation purpose
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        public bool? IsActive { get; set; }

        /// <summary>
        /// UserGroupIdEnum
        /// </summary>
        public int UserGroupId { get; set; }
        
        public string Pin { get; set; }

        public string DisplayName { get; set; }
    }
}