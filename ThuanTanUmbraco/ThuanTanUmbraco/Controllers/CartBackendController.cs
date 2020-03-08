using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ThuanTan.Entity.Context;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class CartBackendController : SurfaceController
    {
        public ActionResult RenderBackEndShoppingCart()
        {
            return PartialView("~/Views/Partials/CartApi/_CartApi.cshtml");
        }

        public ActionResult GetData()
        {
            using (var db = new ThuanTanContext())
            {
                var listCart = db.Carts.Select(x => new CartBackendModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    PaymentMethods = x.PaymentMethods,
                    CreateDate = x.CreateDate
                }).ToList();
                return Json(new
                {
                    data = listCart.AsEnumerable().Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Email,
                        x.PaymentMethods,
                        CreateDate = x.CreateDate.ToString("dd/MM/yyyy")
                    })
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ViewCart(int id = 0)
        {
            if (id > 0)
            {
                using (var db = new ThuanTanContext())
                {
                    var item = db.Carts.FirstOrDefault(x => x.Id == id);
                    if (item != null)
                    {
                        var model = new CartBackendModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Email = item.Email,
                            PhoneNumber = item.PhoneNumber,
                            Address = item.Address,
                            Note = item.Note,
                            PaymentMethods = item.PaymentMethods,
                            ProductCartBackends = db.ProductCarts
                                .Where(y => y.CartId == item.Id)
                                .Select(y => new ProductCartBackend
                                {
                                    CartId = y.CartId,
                                    Quantity = y.Quantity,
                                    Price = y.Price,
                                    ProductId = y.ProductId,
                                    Color = y.Color
                                }).ToList()
                        };
                        return PartialView("~/Views/Partials/CartApi/_ViewCart.cshtml", model);
                    }
                }
            }
            return PartialView("~/Views/Partials/CartApi/_ViewCart.cshtml", new CartBackendModel());
        }
    }
}