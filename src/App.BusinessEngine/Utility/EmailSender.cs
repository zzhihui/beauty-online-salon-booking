using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using App.Data.Entities.Settings;
using SendGrid;

namespace App.BusinessEngine.Utility
{
    public interface IEmailSender
    {
        bool SendPlainEmail(string toEmail, string subject, string body, IEnumerable<string> attachments = null);

        bool SendHtmlEmail(string toEmail, string subject, string htmlBody, IEnumerable<string> attachments = null);
    }
    
    public class EmailSender : IEmailSender
    {
        public bool SendPlainEmail(string toEmail, string subject, string body, IEnumerable<string> attachments = null)
        {
            return SendEmail(toEmail, subject, body, null, attachments);
        }

        public bool SendHtmlEmail(string toEmail, string subject, string htmlBody, IEnumerable<string> attachments = null)
        {
            return SendEmail(toEmail, subject, null, htmlBody, attachments);
        }

        private bool SendEmail(string toEmail, string subject, string body, string bodyHtml, IEnumerable<string> attachments)
        {
            try
            {
                var client = new SmtpClient(EmailHelper.EmailServerSettings.SmtpServer, EmailHelper.EmailServerSettings.SmtpPort)
                {
                    Credentials = new NetworkCredential(EmailHelper.EmailServerSettings.SmtpUserName, EmailHelper.EmailServerSettings.SmtpPassword),
                    EnableSsl = true
                };

                client.Send("noreply@beautyfairy.ie", toEmail, subject, body);
            }
            catch (System.Exception e)
            {
                return false;
            }

            return true;
        }

        private bool SendGridEmail(EmailServerSettings settings, string toEmail, string subject, string body, string bodyHtml, IEnumerable<string> attachments)
        {
            try
            {
                var myMessage = new SendGridMessage();
                myMessage.AddTo(toEmail);
                myMessage.From = new MailAddress(settings.MailFrom);
                myMessage.Subject = subject;
                myMessage.Text = body;
                myMessage.Html = bodyHtml;

                //add attachement
                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        myMessage.AddAttachment(attachment);
                    }
                }
                
                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential(settings.SmtpUserName, settings.SmtpPassword);

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                transportWeb.Deliver(myMessage);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}