using SGE.Cagravol.Presentation.Resources.Common;
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
	public static class TextBoxHelpers
	{
		public static MvcHtmlString TextBoxReadOnlyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, bool isReadOnly, object htmlAttributes = null)
		{
			var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			if (isReadOnly)
			{
				AttributeMaker.AddReadOnly(attrs);
			}
			return helper.TextBoxFor(expression, attrs);
		}

		public static MvcHtmlString TextBoxReadOnlyFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string format, bool isReadOnly, object htmlAttributes = null)
		{
			var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			if (isReadOnly)
			{
				AttributeMaker.AddReadOnly(attrs);
			}
			return helper.TextBoxFor(expression, format, attrs);
		}

		public static MvcHtmlString TextBoxForDateTime<TModel>(this HtmlHelper<TModel> helper, Expression<Func<TModel, DateTime>> expression, bool isReadOnly, object htmlAttributes = null)
		{
			var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			if (isReadOnly)
			{
				AttributeMaker.AddReadOnly(attrs);
			}
			return helper.TextBoxFor(expression, "{0:" + CommonResources.DateTimeBinder_DateTimeFormat + "}", attrs);
		}
	}
}
