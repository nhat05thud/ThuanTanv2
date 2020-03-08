using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThuanTanUmbraco.ClassHelper;

namespace ThuanTanUmbraco.Models
{
    public class CheckOut
    {
        public CheckOut()
        {
            IsSuccess = false;
        }
        public int MemberId { get; set; }
        public string PaymentMethods { get; set; }
        [Required(ErrorMessage = "Nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Nhập địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Nhập email")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string Email { get; set; }
        public string Note { get; set; }
        public List<CartItem> CartItems { get; set; }
        public bool IsSuccess { get; set; }
        public int CultureLcid { get; set; }
    }
}