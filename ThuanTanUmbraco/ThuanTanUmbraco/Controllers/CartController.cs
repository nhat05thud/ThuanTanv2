using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ThuanTanUmbraco.Models;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class CartController : SurfaceController
    {
        public ActionResult InitCart()
        {
            var listCart = new List<CartItem>();
            if (Session["Cart"] != null)
            {
                listCart = (List<CartItem>)Session["Cart"];
            }
            return PartialView("~/Views/Partials/Cart/_CartItem.cshtml", listCart);
        }
        public ActionResult RenderCartItem(string model)
        {
            var cart = new JavaScriptSerializer().Deserialize<List<CartItem>>(model);
            return PartialView("~/Views/Partials/Cart/_CartItem.cshtml", cart);
        }
        public ActionResult RenderCartDetail()
        {
            var cart = new List<CartItem>();
            if (Session["Cart"] != null)
            {
                cart = (List<CartItem>)Session["Cart"];
                Session["Cart"] = cart;
            }
            return PartialView("~/Views/Partials/Cart/CartDetail/_Cart.cshtml", cart);
        }
        public ActionResult KeepShopping()
        {
            var culture = CultureInfo.CurrentCulture;
            var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
            var url = "/";
            if (currentHomePage != null)
            {
                url = new RenderModel(Umbraco.TypedContent(currentHomePage.Id), culture).Content.Url;
            }
            return Json(new { link = url }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GoToPayment()
        {
            var culture = CultureInfo.CurrentCulture;
            var currentHomePage = Services.ContentService.GetRootContent().First(x => x.GetCulture().Name == culture.Name);
            var url = "/checkout";
            var checkout = currentHomePage?.Children().FirstOrDefault(x => x.ContentType.Alias == "checkOut");
            if (checkout != null)
            {
                url = new RenderModel(Umbraco.TypedContent(checkout.Id), culture).Content.Url;
            }
            return Json(new {link = url }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateTotalPrice(int id, int quantity)
        {
            var total = 0;
            if (Session["Cart"] != null)
            {
                var listCart = (List<CartItem>)Session["Cart"];
                var item = listCart.SingleOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.Quantity = quantity > 0 ? quantity : item.Quantity += 1;
                }
                total = listCart.Sum(x => (x.Price * x.Quantity));
                Session["Cart"] = listCart;
            }
            return Json(new { total = total }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public void AddToCart(string model)
        {
            var list = new JavaScriptSerializer().Deserialize<List<CartItem>>(model);
            Session["Cart"] = list;
        }
        [HttpPost]
        public ActionResult DeleteCartItem(int id, string color)
        {
            var itemPrice = 0;
            if (Session["Cart"] != null)
            {
                var listCart = (List<CartItem>)Session["Cart"];
                var item = listCart.SingleOrDefault(x => x.Id == id && x.Color.Trim().Replace(" ", "") == color.Trim().Replace(" ", ""));
                if (item != null)
                {
                    itemPrice = item.Price * item.Quantity;
                    listCart.Remove(item);
                }
                Session["Cart"] = listCart;
            }
            return Json(new { price = itemPrice }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteAllCart()
        {
            var cart = new List<CartItem>();
            Session["Cart"] = cart;
            return PartialView("~/Views/Partials/Cart/CartDetail/_Cart.cshtml", cart);
        }
    }
}