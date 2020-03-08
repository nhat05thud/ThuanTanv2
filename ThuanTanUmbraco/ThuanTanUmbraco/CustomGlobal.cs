using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.Models;

namespace ThuanTanUmbraco
{
    public class CustomGlobal : UmbracoApplication
    {
        public void Init(HttpApplication application)
        {
            base.Init();
        }

        public new void Application_Start(object sender, EventArgs e)
        {
        }

        public void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            //var culture = System.Globalization.CultureInfo.CurrentCulture;
            //var n = ApplicationContext.Current.Services.ContentService.GetRootContent().First(x=>x.GetCulture().Name == culture.Name);
            //var m = n.Children().FirstOrDefault(x => x.ContentType.Alias == "visitor");
            //if (m != null)
            //{
            //    try
            //    {
            //        if (Application["sessionArr"] == null)
            //        {
            //            Application["sessionArr"] = new List<string>();
            //        }
            //        if (!((List<string>)Application["sessionArr"]).Contains(Session.SessionID))
            //        {
            //            Application["OnlineVisitors"] = (int?)Application["OnlineVisitors"] + 1 ?? 1;
            //            ((List<string>)Application["sessionArr"]).Add(Session.SessionID);
            //            var item = m.Children().FirstOrDefault(x =>
            //                x.CreateDate.ToString("dd-MM-yyyy") == DateTime.UtcNow.Date.ToString("dd-MM-yyyy"));
            //            if (item == null)
            //            {
            //                item = ApplicationContext.Current.Services.ContentService.CreateContent("visitor ngày " + DateTime.UtcNow.Date.ToString("dd-MM-yyyy"), m.Id, "visitorItem");
            //                item.SetValue("lbNumber", Application["OnlineVisitors"]);
            //                item.CreateDate = DateTime.UtcNow.Date;
            //            }
            //            else
            //            {
            //                item.SetValue("lbNumber", Convert.ToInt32(item.GetValue("lbNumber")) + 1);
            //            }
            //            ApplicationContext.Current.Services.ContentService.Save(item);
            //            ApplicationContext.Current.Services.ContentService.Publish(item);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // ignore
            //    }
                
            //}
            Application.UnLock();
        }
        public void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            //if ((int)Application["OnlineVisitors"] > 1)
            //{
            //    Application["OnlineVisitors"] = (int)Application["OnlineVisitors"] - 1;
            //    ((List<string>)Application["sessionArr"]).Remove(Session.SessionID);
            //}
            Application.UnLock();
        }
    }
}