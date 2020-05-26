using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Application.Core.Extensions.Models;
using SGE.Cagravol.Application.Core.Extensions.Utils;
using SGE.Cagravol.Presentation.Resources.Common;

namespace SGE.Cagravol.Application.Core.Extensions.RazorHelpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString Menu(this HtmlHelper helper, ICollection<MenuItem> menuItems)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder ulTag, liTag, aTag, iconTag, subMenuDiv;

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("menu");

            string currentAction = helper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = helper.ViewContext.RouteData.GetRequiredString("controller");

            bool isQuickAccessSeleted = false;
            foreach (MenuItem item in menuItems)
            {
                liTag = new TagBuilder("li");
                aTag = new TagBuilder("a");
                iconTag = new TagBuilder("i");
                if (item.SubMenuItems != null)
                {
                    liTag.AddCssClass("hasSubMenu");
                }
                if (item.Type == MenuItemTypeEnum.Home)
                {
                    liTag.AddCssClass("home");
                }
                else if (item.Type == MenuItemTypeEnum.Category)
                {
                    liTag.AddCssClass("header");
                }
                else
                { //Item
                    if (item.Type == MenuItemTypeEnum.Shortcut)
                    {
                        if (isQuickAccessSeleted == false && item.Controller == currentController && item.Action == currentAction)
                        {
                            liTag.AddCssClass("active");
                            isQuickAccessSeleted = true;
                        }
                    }
                    else
                        if (item.Controller == currentController && isQuickAccessSeleted == false)
                        {
                            //Add class active
                            liTag.AddCssClass("active");
                            isQuickAccessSeleted = true;
                        }
                }

                iconTag.AddCssClass(item.Icon);
                if (String.IsNullOrEmpty(item.Controller) && (String.IsNullOrEmpty(item.Action)))
                {
                    aTag.MergeAttribute("onclick", "return false");
                }
                else
                {
                    aTag.MergeAttribute("href", urlHelper.Action(item.Action, item.Controller).ToString());
                }

                aTag.InnerHtml = iconTag.ToString() + item.Name;
                if (item.SubMenuItems != null && item.SubMenuItems.Count > 0)
                {
                    TagBuilder ul, li, a, icon;
                    subMenuDiv = new TagBuilder("div");
                    subMenuDiv.AddCssClass("submenu");
                    ul = new TagBuilder("ul");
                    foreach (var subItem in item.SubMenuItems)
                    {
                        li = new TagBuilder("li");
                        a = new TagBuilder("a");
                        icon = new TagBuilder("i");

                        icon.AddCssClass(subItem.Icon);
                        if (String.IsNullOrEmpty(subItem.Controller) && (String.IsNullOrEmpty(subItem.Action)))
                        {
                            a.MergeAttribute("onclick", "return false");
                        }
                        else
                        {
                            a.MergeAttribute("href", urlHelper.Action(subItem.Action, subItem.Controller).ToString());
                        }

                        a.InnerHtml = icon.ToString() + subItem.Name;
                        li.InnerHtml = a.ToString();
                        ul.InnerHtml += li.ToString();
                    }
                    subMenuDiv.InnerHtml = ul.ToString();
                    liTag.InnerHtml = aTag.ToString() + subMenuDiv.ToString();
                    result.Append(liTag.ToString());
                }
                else
                {
                    liTag.InnerHtml = aTag.ToString();

                    result.Append(liTag.ToString());
                }

            }

            ulTag.InnerHtml = result.ToString();

            return MvcHtmlString.Create(ulTag.ToString());
        }

        public static MvcHtmlString BoolToStringConverter<TModel>(this HtmlHelper<TModel> htmlHelper, bool attribute)
        {
			string result = attribute ? CommonResources.Yes : CommonResources.No;
            return MvcHtmlString.Create(result);
        }

		public static MvcHtmlString InfoTag(this HtmlHelper helper, string infoString, string dataPlacement = "top", string icon = "fa fa-info-circle") 
		
		{
			var template = "&nbsp;&nbsp;<i class='{2}' data-toggle='tooltip' data-placement='{0}' title='{1}' style='white-space:pre;max-width:none;'></i>";
			string result = string.Format(template, dataPlacement, infoString, icon);			
			return MvcHtmlString.Create(result);
		}

		public static MvcHtmlString SwitchTag(this HtmlHelper helper, bool value)
		{
			string template = "<i class='fa fa-toggle-{0} fa-2x' title='{1}'></i>";
			string title = value ? CommonResources.IsActive : CommonResources.IsInactive;
			string result = string.Format(template, value ? "on" : "off", title);
			return MvcHtmlString.Create(result);
		}

		public static MvcHtmlString UserRoleList(this HtmlHelper helper, IEnumerable<IdentityUserRole> userRoles, IDictionary<string,string> roleNames)
		{
			string template = "<span class='label label-primary'>{0}</span>";

			var list = new List<string>();
			foreach (var ur in userRoles)
			{ 
				var name = roleNames[ur.RoleId].Replace(" ", string.Empty);
				list.Add(string.Format(template, SGE.Cagravol.Presentation.Resources.Definitions.RoleDefinitionResources.ResourceManager.GetString(name)));
			}

			string result = string.Join(" ", list.ToArray());
			return MvcHtmlString.Create(result);
		}

		
    }
}
