using Microsoft.Practices.Unity;
using SGE.Cagravol.Application.Core.Extensions.AuthorizationRules;

namespace SGE.Cagravol.Application.Core.Extensions.Filters.AuthorizationRules
{
    public class AdminOnlyAttribute : AuthorizeByRulesAttribute
    {
        [Dependency]
        public IsAdmin IsAdmin { get; set; }

        public AdminOnlyAttribute()
            : base()
        { }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            this.InitializeRules(this.IsAdmin);
            base.OnActionExecuting(filterContext);
        }
    }
}
