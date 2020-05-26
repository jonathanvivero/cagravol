using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace SGE.Cagravol.Application.Core.Extensions.RazorHelpers
{
    internal static class AttributeMaker
    {
        internal static void AddReadOnly(RouteValueDictionary attributes)
        {
            attributes.Add("readonly", "readonly");
        }

        internal static void AddDisabled(RouteValueDictionary attributes)
        {
            attributes.Add("disabled", "disabled");
        }
    }
}
