using System.ComponentModel.DataAnnotations;
using ThuanTanUmbraco.ClassHelper;

namespace ThuanTanUmbraco.Models
{
    public class LoginForm
    {
        public LoginForm()
        {
            IsRunScripts = false;
        }

        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string UsernameLogin { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [MinLength(10, ErrorMessage = "Mật khẩu ít nhất 10 ký tự")]
        [DataType(DataType.Password)]
        public string PasswordLogin { get; set; }

        public string ResponseText { get; set; }
        public string ResponseHeader { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsRunScripts { get; set; }
        public string RedirectLink { get; set; }
        public int LoginCultureLcid { get; set; }
    }
    public class LoginPageForm
    {
        public LoginPageForm()
        {
            IsRunScripts = false;
        }

        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [MinLength(10, ErrorMessage = "Mật khẩu ít nhất 10 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ResponseText { get; set; }
        public string ResponseHeader { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsRunScripts { get; set; }
        public string RedirectLink { get; set; }
        public int CultureLcid { get; set; }
    }

    public class ResetPasswordForm
    {
        [Required(ErrorMessage = "Nhập email")]
        public string Email { get; set; }
        public string ResponseText { get; set; }
        public bool IsSuccess { get; set; }
        public int CultureLcid { get; set; }
    }
    public class RegisterForm
    {
        public RegisterForm()
        {
            IsRegisterSuccess = false;
            IsSendMail = false;
        }
        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [MinLength(10, ErrorMessage = "Mật khẩu ít nhất 10 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Nhập họ")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nhập tên")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Nhập địa chỉ")]
        public string Address { get; set; }
        public string ResponseText { get; set; }
        public bool IsRegisterSuccess { get; set; }
        public bool IsSendMail { get; set; }
        public int CultureLcid { get; set; }
    }

    public class VerifyAccount
    {
        public string Email { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseText { get; set; }
    }
}