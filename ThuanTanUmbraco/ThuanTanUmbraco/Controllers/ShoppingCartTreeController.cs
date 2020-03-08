using System;
using System.Net.Http.Formatting;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace ThuanTanUmbraco.Controllers
{
    [Tree("shoppingCart", "shoppingCartTree", "Shopping Cart Tree", sortOrder: 0)]
    public class ShoppingCartTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            throw new NotImplementedException();
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            var item = CreateTreeNode("cart", id, queryStrings, "Cart", "icon-shopping-basket-alt", false);
            item.RoutePath = "shoppingCart/shoppingCartTree/shoppingCart/cart"; ;
            nodes.Add(item);
            return nodes;
        }
    }
}