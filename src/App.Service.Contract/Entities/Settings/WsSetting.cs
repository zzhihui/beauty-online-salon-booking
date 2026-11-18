namespace App.Service.Contract.Entities.Settings
{
    public class WsSetting:WsEntity
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }
        public bool? IsServer { get; set; }
         
    }
}