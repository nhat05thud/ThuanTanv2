using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Hangfire;
using ThuanTanUmbraco.ClassHelper;
using ThuanTanUmbraco.Mailer;
using ThuanTanUmbraco.TemplateEngine;
using Umbraco.Core;

namespace ThuanTanUmbraco.BackgroundJobs
{
    public class SendMail
    {
        public static string EmailTemplatePath = ConfigurationManager.AppSettings["Mailer.TemplatePath"];
        private readonly DotLiquidTemplate _textTemplate = new DotLiquidTemplate(HostingEnvironment.MapPath(EmailTemplatePath));
        private readonly SmtpEmailSender _emailSender = new SmtpEmailSender(
            smtpHost: ConfigurationManager.AppSettings["SmtpClient.Host"],
            port: Convert.ToInt32(ConfigurationManager.AppSettings["SmtpClient.Port"] ?? "25"),
            useDefaultCredentials: Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpClient.UseDefaultCredentials"] ?? "False"),
            userName: ConfigurationManager.AppSettings["SmtpClient.Username"],
            password: ConfigurationManager.AppSettings["SmtpClient.Password"],
            enableSsl: Convert.ToBoolean(ConfigurationManager.AppSettings["SmtpClient.EnableSsl"] ?? "False"));
        
        public async Task SendMailResetPassword(string email, string title, string newPassword)
        {
            var newMessage = _emailSender.CreateNoReplyEmail();
            newMessage.To = email;
            newMessage.Subject = title;
            newMessage.Body = _textTemplate.Render("resetPassword", new { email, newPassword });
            await _emailSender.SendAsync(newMessage);
        }
        public async Task SendMailVerifyRegister(string email, string title, string domainName, string verifyLink)
        {
            var actionEndPoint = verifyLink + "?query=";
            var encrypData = $"{email}";
            encrypData = CryptoHelper.EncryptString(encrypData);
            var newMessage = _emailSender.CreateNoReplyEmail();
            newMessage.To = email;
            newMessage.Subject = title;
            newMessage.Body = _textTemplate.Render("verifyRegister", new { email, domainName, actionEndPoint, encrypData });
            await _emailSender.SendAsync(newMessage);
        }
        public static void EnqueueForgotPassword(string email, string title, string newPassword)
        {
            BackgroundJob.Enqueue<SendMail>(s => s.SendMailResetPassword(email, title, newPassword));
        }
        public static void EnqueueRegister(string email, string title, string domainName, string verifyLink)
        {
            BackgroundJob.Enqueue<SendMail>(s => s.SendMailVerifyRegister(email, title, domainName, verifyLink));
        }
    }
}