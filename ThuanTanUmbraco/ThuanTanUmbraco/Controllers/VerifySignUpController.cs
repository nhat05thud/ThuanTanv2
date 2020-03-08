using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ThuanTanUmbraco.ClassHelper;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class VerifySignUpController : SurfaceController
    {
        // GET: VerifySignUp
        public ActionResult Verify(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                ViewBag.IsHasQuery = true;
                var email = CryptoHelper.DecryptString(query);
                ViewBag.Email = email;
                var member = Services.MemberService.GetByEmail(email);
                if (member != null)
                {
                    member.IsApproved = true;
                    Services.MemberService.Save(member);
                    ViewBag.IsSuccess = true;
                    ViewBag.ResponseText = "Xác thực email thành công. Bạn vui lòng đăng nhập để tiếp tục.";
                    var culture = CultureInfo.CurrentCulture;
                    var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
                    var signInLink = currentHomePage?.Children().FirstOrDefault(x => x.ContentType.Alias == "memberInfo")?.Children().FirstOrDefault(x => x.ContentType.Alias == "signIn");
                    ViewBag.SignInLink = signInLink != null ? new RenderModel(Umbraco.TypedContent(signInLink.Id), culture).Content.Url : "#";
                    return PartialView("~/Views/Partials/VerifySignUp/_Verify.cshtml");
                }
                ViewBag.IsSuccess = false;
                ViewBag.ResponseText = "Đã có lỗi xảy ra trong quá trình xác thực email. Vui lòng liên hệ quản trị viên để được hỗ trợ.";
            }
            ViewBag.IsHasQuery = false;
            return PartialView("~/Views/Partials/VerifySignUp/_Verify.cshtml");
        }
    }
}