using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.ViewEngines
{
	public class DefaultViewEngine : RazorViewEngine
	{
		public DefaultViewEngine()
		{
			this.AreaViewLocationFormats = new string[]
                                               {
                                                   "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                   "~/Areas/{2}/Views/Common/{0}.cshtml",
                                                   "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                               };
			this.AreaMasterLocationFormats = new string[]
                                                 {
                                                     "~/Areas/{2}/Views/{1}/{0}.cshtml",
													 "~/Areas/{2}/Views/Common/{0}.cshtml",
                                                     "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                                 };
			this.AreaPartialViewLocationFormats = new string[]
                                                      {
                                                          "~/Areas/{2}/Views/{1}/Partials/{0}.cshtml",
                                                          "~/Areas/{2}/Views/Common/Partials/{0}.cshtml",
                                                          "~/Areas/{2}/Views/Shared/Partials/{0}.cshtml",
                                                          "~/Areas/{2}/Views/{1}/{0}.cshtml",              // Support to {area}/{controller}/editortemplates/{editor}.cshtml
                                                      };
			this.ViewLocationFormats = new string[]
                                           {
                                               "~/Views/{1}/{0}.cshtml",
                                               "~/Views/Shared/{0}.cshtml",
                                               "~/Views/Shared/Mailing/{0}.cshtml"
                                           };
			this.MasterLocationFormats = new string[]
                                             {
                                                 "~/Views/{1}/{0}.cshtml",
                                                 "~/Views/Shared/{0}.cshtml",
                                             };
			this.PartialViewLocationFormats = new string[]
                                                  {
                                                      "~/Views/{1}/Partials/{0}.cshtml",
                                                      "~/Views/{1}/{0}.cshtml",              // Support to {controller}/editortemplates/{editor}.cshtml
                                                      "~/Views/Shared/Partials/{0}.cshtml",
                                                      "~/Views/Shared/{0}.cshtml",
                                                      "~/Views/Shared/Mailing/{0}.cshtml"
                                                  };

			this.FileExtensions = new string[] { "cshtml" };
		}
	}
}
