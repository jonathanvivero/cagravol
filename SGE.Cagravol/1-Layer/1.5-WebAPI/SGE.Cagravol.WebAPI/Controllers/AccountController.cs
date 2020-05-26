using Microsoft.AspNet.Identity;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.JSON.Account;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using SGE.Cagravol.Presentation.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Account")]
    [EnableCors(origins: "*", headers: "* ", methods: "* ")]
    public class AccountController : BaseApiController
    {        
        public AccountController(IWebApiContentService webApiContentService)
            : base(webApiContentService)
        {

        }


        #region Methods
        // POST api/Account/CustomerRegister
        [AllowAnonymous]
        [Route("CustomerRegister", Name="CustomerInfo")]
        [HttpPost]
        public async Task<HttpResponseMessage> CustomerRegister(CustomerRegistrationRequest request)
        {

            HttpResponseMessage httpResponse = null;
            Customer cuz = null;
            string uri = string.Empty;
            var rmsCustomer = await this.mainService.RegisterNewCustomerInProjectAsync(request);                      
            IResultServiceModel<CustomerRegistrationResponse> response = new ResultServiceModel<CustomerRegistrationResponse>();

            if (rmsCustomer.Success)
            {
                cuz = rmsCustomer.Value;
                this.mainService.SendEmailNewCustomerSignUp(cuz, this.GetBaseUrl());

                response.OnSuccess(new CustomerRegistrationResponse() { CustomerId = cuz.Id, ProjectId = rmsCustomer.Value.ProjectId });
            }
            else
            {
                response.OnError(rmsCustomer.ErrorMessage, rmsCustomer.ErrorCode);
            }
            
            httpResponse = Request.CreateResponse<IResultServiceModel<CustomerRegistrationResponse>>(HttpStatusCode.Created, response);
            uri = Url.Link("CustomerInfo", new { id = response.ErrorCode });

            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        // POST api/Account/CustomerRegister
        [AllowAnonymous]
        [Route("RegisterReservation", Name = "RegisterReservationApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> RegisterReservation(CustomerRegistrationRequest request)
        {

            HttpResponseMessage httpResponse = null;
            Customer cuz = null;
            string uri = string.Empty;
            var rmsCustomer = await this.mainService.RegisterNewCustomerInProjectAsync(request, false);
            IResultServiceModel<CustomerRegistrationResponse> response = new ResultServiceModel<CustomerRegistrationResponse>();

            if (rmsCustomer.Success)
            {
                cuz = rmsCustomer.Value;
                this.mainService.SendEmailNewCustomerSignUp(cuz, this.GetBaseUrl());

                response.OnSuccess(new CustomerRegistrationResponse() { CustomerId = cuz.Id, ProjectId = rmsCustomer.Value.ProjectId });
            }
            else
            {
                response.OnError(rmsCustomer.ErrorMessage, rmsCustomer.ErrorCode);
            }

            httpResponse = Request.CreateResponse<IResultServiceModel<CustomerRegistrationResponse>>(HttpStatusCode.Created, response);
            uri = Url.Link("RegisterReservationApi", new { id = response.ErrorCode });

            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [AllowAnonymous]
        [Route("UserLogin")]
        [HttpPost]
        public async Task<HttpResponseMessage> UserLogin(UserLoginRequest request)
        {

            HttpResponseMessage httpResponse = null;
            string uri = string.Empty;
            var response = await this.mainService.UserLoginAsync(request);

            if (response.Success)
            {
                httpResponse = Request.CreateResponse<UserLoginResponse>(HttpStatusCode.Created, response.Value);
                uri = Url.Link("MyProfile", new { id = response.Value.CustomerId });
            }
            else
            {
                httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response.ToResultModel());
                uri = Url.Link("ErrorInfo", new { id = response.ErrorCode });
            }

            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }



        

        //private IHttpActionResult GetErrorResult(IdentityResult result)
        //{
        //    if (result == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (!result.Succeeded)
        //    {
        //        if (result.Errors != null)
        //        {
        //            foreach (string error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error);
        //            }
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            // No ModelState errors are available to send, so just return an empty BadRequest.
        //            return BadRequest();
        //        }

        //        return BadRequest(ModelState);
        //    }

        //    return null;
        //}
        #endregion


    }
}
