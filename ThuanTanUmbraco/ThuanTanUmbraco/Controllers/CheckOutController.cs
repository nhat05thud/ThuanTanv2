using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ThuanTan.Entity.Context;
using ThuanTan.Entity.Models;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class CheckOutController : SurfaceController
    {
        private ThuanTanContext db = new ThuanTanContext();
        public ActionResult CheckSessionCart()
        {
            var checkOut = new CheckOut();
            if (Session["Cart"] != null)
            {
                checkOut.CartItems = (List<CartItem>)Session["Cart"];
                Session["Cart"] = checkOut.CartItems;
            }
            return PartialView("~/Views/Partials/Checkout/_checkout.cshtml", checkOut);
        }
        [HttpPost]
        public ActionResult CheckOut(CheckOut model)
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(model.CultureLcid);
                if (!ModelState.IsValid)
                {
                    return PartialView("~/Views/Partials/Checkout/_Form.cshtml", model);
                }
                var cartModel = new Cart
                {
                    MemberId = model.MemberId,
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Note = model.Note,
                    PaymentMethods = model.PaymentMethods,
                    CreateDate = DateTime.Now
                };
                db.Carts.Add(cartModel);
                db.SaveChanges();
                if (model.CartItems != null && model.CartItems.Count > 0)
                {
                    var cartId = cartModel.Id;
                    foreach (var item in model.CartItems)
                    {
                        var productCart = new ProductCart
                        {
                            CartId = cartId,
                            Quantity = item.Quantity,
                            ProductId = item.Id,
                            Price = item.Price,
                            Color = item.Color
                        };
                        db.ProductCarts.Add(productCart);
                    }
                    db.SaveChanges();
                }
                model.IsSuccess = true;
                Session["Cart"] = null;
                return PartialView("~/Views/Partials/Checkout/_Form.cshtml", model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}