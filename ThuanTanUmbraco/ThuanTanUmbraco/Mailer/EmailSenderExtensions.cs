using System.Collections.Generic;
using System.Configuration;

namespace ThuanTanUmbraco.Mailer
{
    public static class EmailSenderExtensions
    {
        public static EmailMessage CreateNoReplyEmail(this IEmailSender emailSender)
        {
            return new EmailMessage
            {
                From = ConfigurationManager.AppSettings["Mailer.EmailFrom"] ?? "noreply@educationfinance.com.vn",
                SenderDisplayName = ConfigurationManager.AppSettings["Mailer.SenderName"] ?? "noreply@educationfinance.com.vn",
                IsBodyHtml = true,
                AttachFiles = new List<string>()
            };
        }
    }
}