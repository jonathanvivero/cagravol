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
    public static class RadioButtonHelpers
    {
        public static MvcHtmlString RadioButtonReadOnlyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool isReadOnly, object value, object htmlAttributes = null)
        {
            var indexedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (isReadOnly)
            {
                AttributeMaker.AddDisabled(indexedAttributes);
                AttributeMaker.AddReadOnly(indexedAttributes);
            }

            StringBuilder result = new StringBuilder();

            var radiobutton = helper.RadioButtonFor(expression, value, indexedAttributes);

            result.Append(radiobutton);

            return MvcHtmlString.Create(result.ToString());
        }
    }
}
