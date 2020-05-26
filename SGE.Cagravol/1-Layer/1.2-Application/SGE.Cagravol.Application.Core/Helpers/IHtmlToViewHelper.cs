using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Helpers
{
	public interface IHtmlToViewHelper
	{
		string GetHtmlFromRazorView(string viewToRender, object viewData, Controller controller);
				
	}
}
