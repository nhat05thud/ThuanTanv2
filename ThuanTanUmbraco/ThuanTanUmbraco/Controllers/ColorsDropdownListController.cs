using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace ThuanTanUmbraco.Controllers
{
    public class ColorsDropdownListController : UmbracoAuthorizedApiController
    {
        // GET: ColorsDropdownList
        public List<IContent> GetAllColors()
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            var home = Services.ContentService.GetRootContent().First();
            var manageProductColors = home?.Children().FirstOrDefault(x => x.ContentType.Alias == "manageProductColors");
            return manageProductColors?.Children().ToList();
        }

    }
}