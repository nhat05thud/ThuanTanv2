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
            var emailReceive = WebConfigurationManager.AppSettings["EmailContactReceive"];
            //var domainName = Request.Url?.GetLeftPart(UriPartial.Authority);
            BackgroundJobs.SendMail.EnqueueContact("Brabantia liên hệ", emailReceive, model.Name, model.Email, model.Phone, model.Message);

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