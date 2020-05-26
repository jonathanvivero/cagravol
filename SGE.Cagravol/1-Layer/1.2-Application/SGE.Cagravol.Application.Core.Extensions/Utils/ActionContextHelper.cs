using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.Utils
{
	internal static class ActionContextHelper
	{
		public static int GetIntParameter(ActionExecutingContext filterContext, string parameterName)
		{
			var currentParameter = filterContext.HttpContext.Request.QueryString == null ? null : filterContext.HttpContext.Request.QueryString[parameterName];
			if (string.IsNullOrEmpty(currentParameter))
			{
				currentParameter = filterContext.HttpContext.Request.Form == null ? null : filterContext.HttpContext.Request.Form[parameterName];
			}
			int parameterValue = 0;

			if (!string.IsNullOrEmpty(currentParameter))
			{
				int.TryParse(currentParameter, out parameterValue);
			}

			return parameterValue;
		}
	}
}
