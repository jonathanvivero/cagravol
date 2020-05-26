using Microsoft.Practices.Unity;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.Extensions.Filters.AdminOrNotBlockAttribute
{
    public abstract class BaseAdminOrUnauthorizeAttribute : ActionFilterAttribute
    {
        
        [Dependency]
        public IAccountService AccountService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!AccountService.IsAdmin)
            {
                if (!this.CheckAuthorize(filterContext))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = new { Message = "Unauthorize" },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                        filterContext.HttpContext.Response.StatusCode = 500;
                        filterContext.HttpContext.Response.StatusDescription = CommonResources.NotPermissions;
                    }
                }
            }
        }

        protected abstract bool CheckAuthorize(ActionExecutingContext filterContext);
    }
}
