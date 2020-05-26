using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace SGE.Cagravol.Application.Core.Helpers
{
	public class HtmlToViewHelper : IHtmlToViewHelper
	{
		public string GetHtmlFromRazorView(string viewToRender, object viewData, Controller controller)
		{
			if (string.IsNullOrEmpty(viewToRender))
			{
				viewToRender = controller.ControllerContext.RouteData.GetRequiredString("action");
			}

			controller.ViewData.Model = viewData;
			var result = string.Empty;

			using (StringWriter stringWriter = new StringWriter())
			{
				ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewToRender);
				ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
				viewResult.View.Render(viewContext, stringWriter);
				result = stringWriter.GetStringBuilder().ToString();
			}
			return result;
		}
	}
}
