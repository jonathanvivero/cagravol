using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Services.Customers;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.JSON.Customers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Presentation.Resources.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SGE.Cagravol.Application.Core.Enums.Common;
using SGE.Cagravol.Domain.JSON.Projects;
using SGE.Cagravol.Domain.POCO.Files;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController
        : BaseApiController
    {
        protected readonly ICustomerService customerService;

        public CustomerController(IWebApiContentService mainService,
            ICustomerService customerService)
            : base(mainService)
        {
            this.customerService = customerService;

        }

        [Authorize]
        [Route("Activity", Name = "CustomerActivityApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetActivity([FromUri]CustomerActivityRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("CustomerActivityApi", new { userName = request.userName }); ;
            string userId = request.userName;
            IResultServiceModel<CustomerActivityResponse> response = new ResultServiceModel<CustomerActivityResponse>();

            try
            {
                var rsmCuz = await this.ValidateCustomerUserCredentials(request.userName);
                if (rsmCuz.Success)
                {
                    var cuz = rsmCuz.Value;
                    var list = this.customerService.ExtractCustomerFileList(cuz.Files);
                    var resp = new CustomerActivityResponse()
                    {
                        list = list,
                        projectId = cuz.ProjectId,
                        isOutOfDate = false,
                        alertMessage = string.Empty,
                        willHaveRecharge = false,
                        hasRecharge = false
                    };

                    if (cuz.Project.LimitForSendingDate < DateTime.Now)
                    {
                        //is out of date
                        resp.isOutOfDate = true;
                        resp.alertMessage = CustomerResources.IsNotPossibleToSendOutOfDate_Format.sf(cuz.Project.LimitForSendingDate);
                    }
                    else if (cuz.Project.ExtraChargeForSendingDate.AddDays(-3) < DateTime.Now && cuz.Project.ExtraChargeForSendingDate > DateTime.Now && cuz.Project.ExtraChargePercentage > 0)
                    {
                        //sending has recharge
                        resp.willHaveRecharge = true;
                        resp.alertMessage = CustomerResources.SendFileWillHaveRecharge_Format.sf(cuz.Project.ExtraChargeForSendingDate, cuz.Project.ExtraChargePercentage);
                    }
                    else if (cuz.Project.ExtraChargeForSendingDate <= DateTime.Now && cuz.Project.ExtraChargePercentage > 0)
                    {
                        //sending has recharge
                        resp.hasRecharge = true;
                        resp.alertMessage = CustomerResources.SendFileHasRecharge_Format.sf(cuz.Project.ExtraChargeForSendingDate, cuz.Project.ExtraChargePercentage);
                    }

                    response.OnSuccess(resp);
                }
                else
                {
                    response.OnError(rsmCuz.ErrorMessage, rsmCuz.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            httpResponse = Request.CreateResponse<IResultServiceModel<CustomerActivityResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("Item", Name = "CustomerItemApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetItem([FromUri]CustomerItemRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("CustomerItemApi", new { userName = request.userName }); ;
            string userId = request.userName;
            IResultServiceModel<CustomerItemResponse> response = new ResultServiceModel<CustomerItemResponse>();

            try
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    var rsmFile = this.mainService.GetCustomerFile(request.id, request.userName);

                    var file = new FilePOCO(rsmFile.Value);

                    response.OnSuccess(new CustomerItemResponse()
                    {
                        item = file
                    });
                }
                else
                {
                    response.OnError(rmCred.ErrorMessage, rmCred.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            httpResponse = Request.CreateResponse<IResultServiceModel<CustomerItemResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("Delete", Name = "CustomerDeleteItemApi")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteItem([FromUri]CustomerFileDeleteRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("CustomerDeleteItemApi", new { id = request.id });
            string userId = request.userName;
            IResultModel response = new ResultModel();

            if (request.id <= 0)
            {
                response.OnError(CustomerResources.ErrorItemDoesNotExist_OnDelete, EnumErrorCode.ITEM_DOES_NOT_EXIST);
            }
            else
            {
                try
                {
                    var rmCred = this.ValidateUserCredentials(request.userName);
                    if (rmCred.Success)
                    {
                        response = await this.customerService.DeleteFileAsync(request);
                    }
                    else
                    {
                        response.OnError(rmCred.ErrorMessage, rmCred.ErrorCode);
                    }
                }
                catch (Exception ex)
                {
                    response.OnException(ex);
                }
            }

            httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("UpdateFile", Name = "CustomerUpdateItemApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateItem([FromBody]CustomerFileUpdateRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("CustomerDeleteItemApi", new { id = request.id });
            IResultModel response = new ResultModel();

            try
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    response = this.mainService.UpdateFile(request.id, request.name, request.fileNotes, request.fileTypeId, request.userName);
                }
                else
                {
                    response.OnError(rmCred.ErrorMessage, rmCred.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }


            httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


    }
}
