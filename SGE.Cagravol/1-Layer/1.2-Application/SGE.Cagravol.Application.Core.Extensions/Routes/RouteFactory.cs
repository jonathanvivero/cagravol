using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGE.Cagravol.Application.Core.Extensions.Routes
{
	public class RouteFactory
	{
		public static void CreateGuidRouteForArea(RouteCollection routes, string name, string path)
		{
			
			routes.Add(string.Format("{0}_default", name), new GuidRoute(
						  string.Format("{0}/{{controller}}/{{action}}/{{id}}", path),
						  new
						  {
							  controller = "Home",
							  action = "Index",
							  id = UrlParameter.Optional
						  },
						  new[] { string.Format("EI.Satellite.Presentation.Web.Areas.{0}.Controllers", name) },
						  name));

			routes.Add(string.Format("{0}_guid", name), new GuidRoute(
						 string.Format("{{guid}}/{0}/{{controller}}/{{action}}/{{id}}", path),
						 new
						 {
							 controller = "Home",
							 action = "Index",
							 id = UrlParameter.Optional
						 },
						 new[] { string.Format("EI.Satellite.Presentation.Web.Areas.{0}.Controllers", name) },
						 name));
		}

		public static void CreateGuidRoute(RouteCollection routes, string name)
		{
			routes.Add(string.Format("{0}_default", name), new GuidRoute(
						  "{controller}/{action}/{id}",
						  new
						  {
							  controller = "Home",
							  action = "Index",
							  id = UrlParameter.Optional
						  },
						  new[] { "EI.Satellite.Presentation.Web.Controllers" }));

			routes.Add(string.Format("{0}_guid", name), new GuidRoute(
						  "{guid}/{controller}/{action}/{id}",
						  new
						  {
							  controller = "Home",
							  action = "Index",
							  id = UrlParameter.Optional
						  },
						  new[] { "EI.Satellite.Presentation.Web.Controllers" }));
		}
	}
}
