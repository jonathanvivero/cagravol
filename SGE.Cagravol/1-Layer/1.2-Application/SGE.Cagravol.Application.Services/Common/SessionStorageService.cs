using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace SGE.Cagravol.Application.Services.Common
{
	public class SessionStorageService : ISessionStorageService
	{
		private readonly RequestContext requestContext;
		public SessionStorageService(RequestContext httpContext)
		{
			this.requestContext = httpContext;
		}

		public T Get<T>(string name)
		{
			var key = this.GenerateKey(name);

			return (T)this.requestContext.HttpContext.Session[key];
		}

		public void Set(string name, object value)
		{
			var key = this.GenerateKey(name);

			this.requestContext.HttpContext.Session[key] = value;
		}

		private string GenerateKey(string name)
		{
			return string.Format("{0};{1}", this.requestContext.RouteData.Values["guid"], name);
		}
	}
}
