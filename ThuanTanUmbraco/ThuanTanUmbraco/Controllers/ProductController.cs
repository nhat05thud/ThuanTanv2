using System.Globalization;
using System.Web.Mvc;
using ThuanTanUmbraco.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace ThuanTanUmbraco.Controllers
{
    public class ProductController : SurfaceController
    {
        public ActionResult RenderProductItem(int id, string color)
        {
            var model = new RenderModel(Umbraco.TypedContent(id), CultureInfo.CurrentUICulture);
            var productModel = new Product
            {
                RenderProduct = model,
                Color = color
            };
            return PartialView("/Views/Partials/Product/_ImagesWithColor.cshtml", productModel);
        }
    }
}