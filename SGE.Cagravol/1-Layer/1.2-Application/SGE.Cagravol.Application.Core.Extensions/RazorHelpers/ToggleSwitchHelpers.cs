using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Reflection;
using System.Web.Routing;
using System.Dynamic;

namespace SGE.Cagravol.Application.Core.Extensions.RazorHelpers
{
    public static class ToggleSwitchHelpers
    {
        public static MvcHtmlString ToggleSwitchFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            return htmlHelper.ToggleSwitchFor(expression, false, htmlAttributes);
        }

        public static MvcHtmlString ToggleSwitchFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, bool isReadOnly, object htmlAttributes = null)
        {
            //<label>
            //  <input type="checkbox" class="toggle-switch" name="something" id="something" />
            //  <input type="hidden" name="something" value="false" />
            //  <span class="switch"></span>
            //</label>
            var indexedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (!indexedAttributes.Any())
            {
                indexedAttributes.Add("class", "toggle-switch");
            }

            if (isReadOnly)
            {
                AttributeMaker.AddDisabled(indexedAttributes);
            }

            StringBuilder result = new StringBuilder();

            var checkbox = htmlHelper.CheckBoxFor(expression, indexedAttributes);

            result.Append("<label class='toggle-switch-label'>");
            result.Append(checkbox);
            result.Append("<span class=\"switch\"></span>");
            result.Append("</label>");

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
