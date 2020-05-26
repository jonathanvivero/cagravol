using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Application.Services.Identity;
using SGE.Cagravol.Application.Services.Logs;
using SGE.Cagravol.Application.Services.Utils;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Presentation.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.WebAPI.Controllers
{

    public class ErrorMessageEventArgs : EventArgs
    {
        private string errorSource;
        private string errorMessage;        

        public ErrorMessageEventArgs(string es)
        {
            this.errorSource = es;
            this.errorMessage = string.Empty;
        }
        public string Message
        {
            set { this.errorMessage = value;}
            get { return this.errorMessage; }
        }
 
        public string Code
        {
            get { return errorSource; }
        }

    }

    public class BaseApiController : ApiController
    {
        protected readonly IWebApiContentService mainService;
        protected readonly IHtmlToViewHelper htmlToViewHelper;
        protected readonly IUtilService utilService;
        protected readonly ILogService logService;
        protected readonly IAccountService accountService;
        public event EventHandler<ErrorMessageEventArgs> RaiseErrorMessageEvent;

        public BaseApiController(IWebApiContentService mainService)
            : base()
        {
            this.mainService = mainService;
            this.htmlToViewHelper = mainService.HtmlToViewHelper;
            this.utilService = mainService.UtilService;
            this.logService = mainService.LogService;
            this.accountService = mainService.AccountService;            
        }

        protected string GetBaseUrl()
        {
            string protocol = "http", host = "", port = "80";

            protocol = this.Request.RequestUri.Scheme;
            host = this.Request.RequestUri.Host;
            port = this.Request.RequestUri.Port.ToString();


            if (port == "80")
            {
                return "{0}://{1}/".sf(protocol, host);
            }
            else
            {
                return "{0}://{1}:{2}/".sf(protocol, host, port);
            }
        }




        protected string GetErrorsFromModelState() 
        {
            string finalErrorMessage = string.Empty;
            var errors = new List<string>();
            //foreach (ModelState modelState in ModelState.Values)

            //foreach (ModelState modelKeys in ModelState.Keys)
            foreach (KeyValuePair<string,ModelState> modelState in ModelState)
            {
                foreach (ModelError error in modelState.Value.Errors)
                {
                    if (string.IsNullOrWhiteSpace(error.ErrorMessage))
                    {
                        var eh = new ErrorMessageEventArgs(modelState.Key);
                         RaiseErrorMessageEvent(this, eh);
                         errors.Add(eh.Message);                        
                    }
                    else 
                    { 
                        errors.Add(error.ErrorMessage);
                    }                        
                    //var s = this.GetStringFromValidators(error.ErrorMessage);
                }
            }

            finalErrorMessage = ErrorResources.ModelStateErrors_Format.sf(string.Join("", errors.Select(s=> string.Format("<li>{0}</li>", s))));

            return finalErrorMessage;
        }

        protected IResultModel ValidateUserCredentials(string userName)
        {
            IResultModel rm = new ResultModel();

            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    if (claims.HasClaim("sub", userName))
                    {
                        rm.OnSuccess();
                    }
                    else
                    {
                        rm.OnError(ErrorResources.UserCredentialsInvalid, EnumErrorCode.USER_NOT_VALID);
                    }
                }
                else
                {
                    rm.OnError(ErrorResources.UserCredentialsInvalid, EnumErrorCode.USER_NOT_VALID);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        protected async Task<IResultServiceModel<Customer>> ValidateCustomerUserCredentials(string userName)
        {
            IResultServiceModel<Customer> rsm = new ResultServiceModel<Customer>();

            try
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var claims = (ClaimsIdentity)HttpContext.Current.User.Identity;
                    if (claims.HasClaim("sub", userName))
                    {
                        //Now check if this user is in a project and which one of active ones                                               
                        rsm = await this.mainService.CheckProjectForLoggedCustomerByName(userName);
                    }
                    else
                    {
                        rsm.OnError(ErrorResources.UserCredentialsInvalid, EnumErrorCode.USER_NOT_VALID);
                    }
                }
                else
                {
                    rsm.OnError(ErrorResources.UserCredentialsInvalid, EnumErrorCode.USER_NOT_VALID);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            return rsm;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.mainService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
