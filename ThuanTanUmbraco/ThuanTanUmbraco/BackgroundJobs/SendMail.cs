using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Hosting;
using Hangfire;
using ThuanTanUmbraco.ClassHelper;
using ThuanTanUmbraco.Mailer;
using ThuanTanUmbraco.Models;
using ThuanTanUmbraco.TemplateEngine;

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
        public async Task SendMailContact(string title, string emailReceive, string name, string phone, string email, string message)
        {
            var newMessage = _emailSender.CreateNoReplyEmail();
            newMessage.To = email;
            newMessage.Subject = title;
            newMessage.Body = _textTemplate.Render("emailContact", new { title, name, phone, email, message });
            await _emailSender.SendAsync(newMessage);
        }
        public async Task SendMailCheckOut(string emailReceive, string title, string name, string email, string phone, string address, string note, string paymentMethods, List<CartItem> cartItems)
        {
            var cartItemsString = new List<string>();
            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    var objectString = item.Name + "(" + item.Color + ") x" + item.Quantity;
                    cartItemsString.Add(objectString);
                }
            }
            var newMessage = _emailSender.CreateNoReplyEmail();
            newMessage.To = emailReceive;
            newMessage.Subject = title;
            newMessage.Body = _textTemplate.Render("checkOut", new { title, name, email, phone, address, note, paymentMethods, cartItemsString });
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
        public static void EnqueueContact(string title, string emailReceive, string name, string phone, string email, string message)
        {
            BackgroundJob.Enqueue<SendMail>(s => s.SendMailContact(title, emailReceive, name, phone, email, message));
        }
        public static void EnqueueCheckOut(string emailReceive, string title, string name, string email, string phone, string address, string note, string paymentMethods, List<CartItem> cartItems)
        {
            BackgroundJob.Enqueue<SendMail>(s => s.SendMailCheckOut(emailReceive, title, name, email, phone, address, note, paymentMethods, cartItems));
        }
    }
}