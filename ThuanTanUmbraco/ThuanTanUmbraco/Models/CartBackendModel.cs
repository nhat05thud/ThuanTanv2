using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuanTanUmbraco.Models
{
    public class CartBackendModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string PaymentMethods { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<ProductCartBackend> ProductCartBackends { get; set; }
    }
}