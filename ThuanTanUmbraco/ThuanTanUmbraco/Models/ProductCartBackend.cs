using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuanTanUmbraco.Models
{
    public class ProductCartBackend
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int Price { get; set; }
        public string Color { get; set; }
    }
}