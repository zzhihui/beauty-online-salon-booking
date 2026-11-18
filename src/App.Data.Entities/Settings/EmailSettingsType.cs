namespace App.Data.Entities.Settings
{
    public class EmailServerSettings
    {
        public bool SmtpEnabled { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpServer { get; set; }
        public string MailFrom { get; set; }
        public int SmtpPort { get; set; }
    }
}