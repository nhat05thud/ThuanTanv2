using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuanTanUmbraco.ClassHelper;
using ThuanTanUmbraco.Models;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class MemberFrontEndController : SurfaceController
    {
        [HttpPost]
        public ActionResult MemberLogin(LoginForm model)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.LoginCultureLcid);
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
                }
                var member = Services.MemberService.GetByEmail(model.UsernameLogin);
                if (member != null && member.IsApproved)
                {
                    var culture = CultureInfo.CurrentCulture;
                    var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
                    var link = "/";
                    if (currentHomePage != null)
                    {
                        link = new RenderModel(Umbraco.TypedContent(currentHomePage.Id), culture).Content.Url;
                    }
                    if (!Members.Login(model.UsernameLogin, model.PasswordLogin))
                    {
                        model.IsSuccess = false;
                        model.IsRunScripts = true;
                        model.ResponseText = Umbraco.GetDictionaryValue("Login.Failure");
                        model.ResponseHeader = "Error!!";
                        return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
                    }
                    model.IsSuccess = true;
                    model.IsRunScripts = true;
                    model.ResponseText = Umbraco.GetDictionaryValue("Login.Success");
                    model.ResponseHeader = "Success!!";
                    model.RedirectLink = link;
                    return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
                }
                model.IsSuccess = false;
                model.IsRunScripts = true;
                model.ResponseText = Umbraco.GetDictionaryValue("Account.NotExits");
                model.ResponseHeader = "Error!!";
                return PartialView("~/Views/Partials/User/Login/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost]
        public ActionResult MemberLoginPage(LoginPageForm model)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.CultureLcid);
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Partials/User/Login/LoginPage/_Form.cshtml", model);
                }
                var member = Services.MemberService.GetByEmail(model.Username);
                if (member != null && member.IsApproved)
                {
                    var culture = CultureInfo.CurrentCulture;
                    var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
                    var link = "/";
                    if (currentHomePage != null)
                    {
                        link = new RenderModel(Umbraco.TypedContent(currentHomePage.Id), culture).Content.Url;
                    }
                    if (!Members.Login(model.Username, model.Password))
                    {
                        model.IsSuccess = false;
                        model.IsRunScripts = true;
                        model.ResponseText = Umbraco.GetDictionaryValue("Login.Failure");
                        model.ResponseHeader = "Error!!";
                        return PartialView("~/Views/Partials/User/Login/LoginPage/_Form.cshtml", model);
                    }
                    model.IsSuccess = true;
                    model.IsRunScripts = true;
                    model.ResponseText = Umbraco.GetDictionaryValue("Login.Success");
                    model.ResponseHeader = "Success!!";
                    model.RedirectLink = link;
                    return PartialView("~/Views/Partials/User/Login/LoginPage/_Form.cshtml", model);
                }
                model.IsSuccess = false;
                model.IsRunScripts = true;
                model.ResponseText = Umbraco.GetDictionaryValue("Account.NotExits");
                model.ResponseHeader = "Error!!";
                return PartialView("~/Views/Partials/User/Login/LoginPage/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost]
        public ActionResult MemberForgotPassword(ResetPasswordForm model)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.CultureLcid);
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Partials/User/ForgotPassword/_Form.cshtml", model);
                }
                var member = Services.MemberService.GetByEmail(model.Email);
                if (member != null && member.IsApproved)
                {
                    var newPassword = Utils.CreateRandomPassword(10);
                    Services.MemberService.SavePassword(member, newPassword);
                    BackgroundJobs.SendMail.EnqueueForgotPassword(model.Email, Umbraco.GetDictionaryValue("Title.ResetPassword"), newPassword);
                    model.IsSuccess = true;
                    model.ResponseText = Umbraco.GetDictionaryValue("SendMail.NewPassword.Success");
                    return PartialView("~/Views/Partials/User/ForgotPassword/_Form.cshtml", model);
                }
                model.IsSuccess = false;
                model.ResponseText = Umbraco.GetDictionaryValue("Account.NotExits");
                return PartialView("~/Views/Partials/User/ForgotPassword/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult MemberSignUp(RegisterForm model)
        {
            try
            {
                var domainName = Request.Url?.GetLeftPart(UriPartial.Authority);
                var culture = CultureInfo.CurrentCulture;
                var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
                var verifySignUp = currentHomePage?.Children().FirstOrDefault(x => x.ContentType.Alias == "memberInfo")?.Children().FirstOrDefault(x => x.ContentType.Alias == "verifySignUp");
                var verifyLink = "";
                if (verifySignUp != null)
                {
                    verifyLink = new RenderModel(Umbraco.TypedContent(verifySignUp.Id), culture).Content.Url;
                }
                if (model.IsSendMail == false)
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(model.CultureLcid);
                    if (!ModelState.IsValid)
                    {
                        return PartialView("~/Views/Partials/User/SignUp/_Form.cshtml", model);
                    }
                    var member = Services.MemberService.GetByEmail(model.Username);
                    if (member == null)
                    {
                        var newMember = Services.MemberService.CreateMember(model.Username, model.Username, model.FirstName + " " + model.LastName, "Member");
                        newMember.SetValue("firstName", model.FirstName);
                        newMember.SetValue("lastName", model.LastName);
                        newMember.SetValue("phoneNumber", model.PhoneNumber);
                        newMember.SetValue("address", model.Address);
                        newMember.IsApproved = false;
                        newMember.Name = model.FirstName + " " + model.LastName;
                        Services.MemberService.Save(newMember);
                        Services.MemberService.SavePassword(newMember, model.Password);
                        model.IsRegisterSuccess = true;
                        model.IsSendMail = true;
                        BackgroundJobs.SendMail.EnqueueRegister(model.Username, "Email xác nhận tài khoản", domainName, verifyLink);
                        return PartialView("~/Views/Partials/User/SignUp/_Form.cshtml", model);
                    }
                    model.IsRegisterSuccess = false;
                    model.ResponseText = Umbraco.GetDictionaryValue("Email.Exits");
                    return PartialView("~/Views/Partials/User/SignUp/_Form.cshtml", model);
                }
                model.IsRegisterSuccess = true;
                model.IsSendMail = true;
                BackgroundJobs.SendMail.EnqueueRegister(model.Username, "Email xác nhận tài khoản", domainName, verifyLink);
                return PartialView("~/Views/Partials/User/SignUp/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            var culture = CultureInfo.CurrentCulture;
            var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
            var link = "/";
            if (currentHomePage != null)
            {
                link = new RenderModel(Umbraco.TypedContent(currentHomePage.Id), culture).Content.Url;
            }
            Members.Logout();
            return Redirect(link);
        }
    }
}