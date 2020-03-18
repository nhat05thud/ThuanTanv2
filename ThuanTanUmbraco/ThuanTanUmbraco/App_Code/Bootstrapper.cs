using System.Web.Optimization;
using Umbraco.Core;

namespace ThuanTanUmbraco
{
    public class Bootstrapper : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            base.ApplicationStarting(umbracoApplication, applicationContext);
        }
    }
}
