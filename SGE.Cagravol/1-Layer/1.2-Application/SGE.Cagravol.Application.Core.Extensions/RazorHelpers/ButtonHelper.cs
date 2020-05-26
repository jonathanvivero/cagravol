using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.RazorHelpers
{
	public static class ButtonHelper
	{
		public static MvcHtmlString ButtonForAddEditSelectItem<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string URLAction, bool isReadOnly, object htmlAttributes = null)
		{
			TagBuilder aTag = new TagBuilder("a");
			TagBuilder result = new TagBuilder("button");

			var fieldName = ExpressionHelper.GetExpressionText(expression);
			var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

			if (isReadOnly)
			{
				AttributeMaker.AddReadOnly(attrs);
				AttributeMaker.AddDisabled(attrs);
			}
			else
			{
				aTag.Attributes.Add("href", URLAction);
				aTag.Attributes.Add("data-origin", fieldName.Replace('.', '_'));
				result = aTag;
			}

			foreach (var attr in attrs)
			{
				result.Attributes.Add(attr.Key, attr.Value.ToString());
			}

			return MvcHtmlString.Create(result.ToString());
		}

	}
}
