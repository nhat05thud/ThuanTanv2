using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;

namespace ThuanTanUmbraco
{
    public class OnSavingEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //RouteTable.Routes.MapRoute(
            //    name: "ShoppingCartTree",
            //    url: "shoppingCart/Index",
            //    defaults: new
            //    {
            //        controller = "CartBackendApi",
            //        action = "RenderBackEndShoppingCart",
            //        query = UrlParameter.Optional
            //    });
        }
    }
}