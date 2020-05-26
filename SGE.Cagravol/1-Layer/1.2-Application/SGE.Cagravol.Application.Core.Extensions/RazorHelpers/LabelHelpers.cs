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
    public static class LabelHelpers
    {
        public static MvcHtmlString LabelWithRequiredFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            StringBuilder result = new StringBuilder();
            object htmlAttributes = null;
            if (metadata.IsRequired)
            {
                htmlAttributes = new { @class = "required-field" };
            }
            var label = htmlHelper.LabelFor(expression, htmlAttributes);

            result.Append(label);

            return MvcHtmlString.Create(result.ToString());
        }

    }
}
