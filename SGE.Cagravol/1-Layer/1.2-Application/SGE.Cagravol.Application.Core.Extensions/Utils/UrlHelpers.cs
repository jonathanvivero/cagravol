using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.Utils
{
	public static class UrlHelpers
	{
		public static string GetDomain(HttpRequestBase request)
		{
			var domain = string.Concat(request.Url.Scheme, System.Uri.SchemeDelimiter, request.Url.Host, (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port));
			return domain;
		}

		public static string GetBaseUrl(HttpRequestBase request)
		{
			var baseUrl = request.Url.AbsoluteUri.Replace(request.Url.PathAndQuery, string.Empty);
			return baseUrl;
		}

		public static string ContentAbsoluteUrl(this UrlHelper url, string relativeContentPath)
		{
			var baseUri = GetBaseUrl(url.RequestContext.HttpContext.Request);

			return string.Format("{0}{1}", baseUri, VirtualPathUtility.ToAbsolute(relativeContentPath));
		}
	}
}
