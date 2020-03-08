using System.ComponentModel.DataAnnotations;
using ThuanTanUmbraco.ClassHelper;

namespace ThuanTanUmbraco.Models
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Nhập họ tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Nhập nội dung tin nhắn")]
        public string Message { get; set; }
        public string ErrorMsg { get; set; }
        public int CultureLcid { get; set; }
    }
}