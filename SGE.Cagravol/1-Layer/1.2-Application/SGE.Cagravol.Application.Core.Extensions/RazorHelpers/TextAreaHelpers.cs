using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SGE.Cagravol.Application.Core.Extensions.RazorHelpers
{
    public static class TextAreaHelpers
    {
        public static MvcHtmlString TextAreaReadOnlyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool isReadOnly, object htmlAttributes = null)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (isReadOnly)
            {
                AttributeMaker.AddReadOnly(attrs);
            }
            return helper.TextAreaFor(expression, attrs);
        }
    }
}
