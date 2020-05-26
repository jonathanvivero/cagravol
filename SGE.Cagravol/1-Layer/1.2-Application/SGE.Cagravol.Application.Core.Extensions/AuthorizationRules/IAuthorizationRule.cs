using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.AuthorizationRules
{
    public interface IAuthorizationRule
    {
        bool IsValid(ActionExecutingContext filterContext);
    }
}
