using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.JSON.Alfred;
using SGE.Cagravol.Domain.JSON.Files;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Upload")]
    public class UploadController : BaseApiController
    {
        private readonly IFileService fileService;        
        public UploadController(IWebApiContentService mainService, 
            IFileService fileService)
            : base(mainService)
        {
            this.fileService = fileService;
        }       

        [Authorize]
        [Route("Init", Name = "UploadInitApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadInit([FromBody]FileUploadRequest request)
        {
            HttpResponseMessage httpResponse;
            IResultServiceModel<FileUploadResponse> response = new ResultServiceModel<FileUploadResponse>();
            string uri = Url.Link("UploadInitApi", new { id = 0 });

            response = await this.mainService.FileUploadInitAsync(request);
            
            httpResponse = Request.CreateResponse<IResultServiceModel<FileUploadResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("Hash", Name = "UploadHashApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadHash(FileHashUploadRequest request)
        {
            HttpResponseMessage httpResponse;
            IResultServiceModel<FileHashUploadResponse> response = new ResultServiceModel<FileHashUploadResponse>();
            string uri = Url.Link("UploadHashApi", new { id = 0 });

            response = await this.mainService.UploadFileHashAsync(request);

            if (!response.Success) {
                Console.Write("error");
            }
            
            httpResponse = Request.CreateResponse<IResultServiceModel<FileHashUploadResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("End", Name = "UploadEndApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadEnd([FromBody]FileEndUploadRequest request)
        {
            HttpResponseMessage httpResponse;
            IResultServiceModel<FileEndUploadResponse> response = new ResultServiceModel<FileEndUploadResponse>();

            var rmCustomer = await this.ValidateCustomerUserCredentials(request.userName);
            string uri = Url.Link("UploadEndApi", new { id = 0 });

            if (!rmCustomer.Success)
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    rmCustomer = this.mainService.GetCustomerForProjectGeneralSpace(request.projectId, request.userName);
                }
            }

            if (rmCustomer.Success)
            {
                var host = this.GetBaseUrl();
                if (request.fileId > 0)
                {
                    response = await this.mainService.EndFileResendUploadAsync(request, host);            
                }
                else
                { 
                    response = await this.mainService.EndFileUploadAsync(request, host);            
                }
            }
            else
            {
                response.OnError(rmCustomer.ErrorMessage, rmCustomer.ErrorCode);
            }

            
            httpResponse = Request.CreateResponse<IResultServiceModel<FileEndUploadResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("Cancel", Name = "UploadCancelApi")]
        [HttpPost]
        public HttpResponseMessage UploadCancel([FromBody]FileCancelUploadRequest request)
        {
            HttpResponseMessage httpResponse;
            IResultModel response = new ResultModel();
            string uri = Url.Link("UploadCancelApi", new { id = 0 });

            response = this.mainService.FileCancelUpload(request);

            httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }
    }
}
