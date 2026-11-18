using System.Configuration;
using App.Data.Entities.Settings;

namespace App.BusinessEngine.Utility
{
    public static class EmailHelper
    {
        public static EmailServerSettings EmailServerSettings
        {
            get
            {
                //bool.TryParse(ConfigurationManager.AppSettings["SmtpEnabled"], out var smtpEnabled);

                return new EmailServerSettings
                {
                    MailFrom = ConfigurationManager.AppSettings["SmtpFromEmail"],
                    SmtpEnabled = true,
                    SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"],
                    SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]),
                    SmtpServer = ConfigurationManager.AppSettings["SmtpServer"],
                    SmtpUserName = ConfigurationManager.AppSettings["SmtpUserName"]
                };
            }
        }
    }
}
