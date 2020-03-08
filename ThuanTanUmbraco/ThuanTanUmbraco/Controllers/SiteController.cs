using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Mvc;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class SiteController : SurfaceController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult HandleContactForm(ContactForm model)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(model.CultureLcid);
            if (!ModelState.IsValid)
            {
                model.ErrorMsg = Umbraco.GetDictionaryValue("Send.Failure");
                return PartialView("~/Views/Partials/Contact/_Form.cshtml", model);
            }
            var domainName = Request.Url?.GetLeftPart(UriPartial.Authority);
            var emailReceive = WebConfigurationManager.AppSettings["EmailContactReceive"];
            var messageString = "<h3>" + domainName + " LIÊN HỆ</h3>";
            messageString += "<b>Họ Tên: </b>" + model.Name + "<br />";
            messageString += "<b>Email: </b>" + model.Email + "<br />";
            messageString += "<b>Điện thoại: </b>" + model.Phone + "<br />";
            messageString += "<b>Nội dung tin nhắn: </b>" + model.Message;
            var email = new MailMessage
            {
                Subject = "Nội dung liên hệ",
                Body = messageString,
                IsBodyHtml = true,
                To = { emailReceive }
            };
            try
            {
                //Connect to SMTP credentials set in web.config
                var smtp = new SmtpClient();

                //Try & send the email with the SMTP settings
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            model.Name = "";
            model.Email = "";
            model.Phone = "";
            model.Message = "";
            model.ErrorMsg = Umbraco.GetDictionaryValue("Send.Success");
            ModelState.Clear();
            return PartialView("~/Views/Partials/Contact/_Form.cshtml", model);
        }
    }
}