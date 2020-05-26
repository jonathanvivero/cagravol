using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGE.Cagravol.Application.Core.Extensions.Routes
{
	/// <summary>
	/// Custom Route to enable Guids.
	/// <see cref="http://blog.gauffin.org/2012/02/get-a-unique-session-in-each-browser-tab/"/>
	/// </summary>
	public sealed class GuidRoute : Route
	{
		private const string guid = "guid";
		private const string controller = "controller";
		private const string action = "action";
		private const string id = "id";
		private const string area = "area";

		private readonly bool isGuidRoute;
		private readonly bool isArea;


		public GuidRoute(string uri, object defaults)
			: base(uri, new RouteValueDictionary(defaults), new MvcRouteHandler())
		{
			this.isGuidRoute = uri.Contains(guid);

			this.DataTokens = new RouteValueDictionary();
		}

		public GuidRoute(string uri, object defaults, string[] namespaces)
			: this(uri, defaults)
		{
			// Key from https://github.com/ASP-NET-MVC/aspnetwebstack/blob/master/src/System.Web.Mvc/Routing/RouteDataTokenKeys.cs
			this.DataTokens["Namespaces"] = namespaces;
		}

		public GuidRoute(string uri, object defaults, string[] namespaces, string areaName)
			: this(uri, defaults, namespaces)
		{
			// Key from https://github.com/ASP-NET-MVC/aspnetwebstack/blob/master/src/System.Web.Mvc/Routing/RouteDataTokenKeys.cs
			this.DataTokens[area] = areaName;
			this.DataTokens["UseNamespaceFallback"] = true;
			this.isArea = true;
		}

		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			var oldRoute = base.GetRouteData(httpContext);

			var routeData = this.ParseRouteData(httpContext);

			if (routeData == null)
				return null;

			if (!routeData.Values.ContainsKey(guid) || routeData.Values[guid].ToString() == string.Empty)
			{
				routeData.Values[guid] = Guid.NewGuid().ToString("N");
			}

			return routeData;
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			var result = !this.isGuidRoute ? null : base.GetVirtualPath(requestContext, values);

			return result;
		}

		private RouteData ParseRouteData(HttpContextBase httpContext)
		{
			RouteData routeData = new RouteData(this, this.RouteHandler);

			var parsedRoute = httpContext
				.Request
				.Url
				.LocalPath
				.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
				.ToArray();

			if (!parsedRoute.Any())
			{
				if (!isArea)
				{
					this.SetDefaultValuesToRoute(routeData);
				}
				else
				{
					routeData = null;
				}
			}
			else
			{
				var baseIndex = 0;
				var guidToParse = parsedRoute.ElementAt(baseIndex++);

				Guid guidValue = Guid.Empty;

				if (Guid.TryParse(guidToParse, out guidValue))
				{
					routeData.Values.Add(guid, guidToParse.ToString());
				}
				else
				{
					baseIndex--;
				}

				var areaName = this.GetElementAt(parsedRoute, baseIndex++);

				if (isArea && areaName == (string)this.DataTokens[area])
				{
					this.UpdateRoute(routeData, parsedRoute, baseIndex++);
				}
				else
				{
					if ((!isArea && guidValue != Guid.Empty) || (isArea && guidValue == Guid.Empty))
					{
						baseIndex--;
						this.UpdateRoute(routeData, parsedRoute, baseIndex++);
					}
					else
					{
						routeData = null;
					}
				}
			}

			this.CopyDataTokens(routeData);

			return routeData;
		}

		private void CopyDataTokens(RouteData routeData)
		{
			if (routeData != null)
			{
				if (this.DataTokens != null)
				{
					foreach (var prop in this.DataTokens)
					{
						routeData.DataTokens[prop.Key] = prop.Value;
					}
				}
			}
		}

		private void SetDefaultValuesToRoute(RouteData routeData)
		{
			routeData.Values.Add(controller, this.Defaults[controller]);
			routeData.Values.Add(action, this.Defaults[action]);
		}

		private string GetElementAt(string[] array, int index)
		{
			return array.Skip(index).Take(1).SingleOrDefault();
		}

		private void UpdateRoute(RouteData routeData, string[] parsedRoute, int fromParsedElementIndex)
		{
			var controllerName = this.GetElementAt(parsedRoute, fromParsedElementIndex);
			var actionName = this.GetElementAt(parsedRoute, fromParsedElementIndex + 1);
			var idName = this.GetElementAt(parsedRoute, fromParsedElementIndex + 2);

			routeData.Values.Add(controller, controllerName ?? this.Defaults[controller]);
			routeData.Values.Add(action, actionName ?? this.Defaults[action]);

			if (idName != null)
			{
				routeData.Values.Add(id, idName);
			}
		}
	}
}
