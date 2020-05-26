using SGE.Cagravol.Application.Services.Identity;
using System;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.AuthorizationRules
{
    public class IsAdmin : IAuthorizationRule
    {
        [Dependency]
        public IAccountService AccountService { get; set; }

        public bool IsValid(ActionExecutingContext filterContext)
        {
            return this.AccountService.IsAdmin;
        }
    }
}
