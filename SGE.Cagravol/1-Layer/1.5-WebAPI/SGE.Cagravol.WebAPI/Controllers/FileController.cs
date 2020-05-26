using SGE.Cagravol.Application.Services.Files;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.JSON.Files;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/File")]
    public class FileController
        : BaseApiController
    {

        private readonly ICustomerFileService customerFileService;


        public FileController(IWebApiContentService mainService,
            ICustomerFileService customerFileService)
            : base(mainService)
        {
            this.customerFileService = customerFileService;
        }

        [Authorize]
        [Route("", Name = "FilesApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(ProjectFilesListRequest request)
        {   

            //var request = new ProjectFilesListRequest() { userName = userName, projectId = 0 };

            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("FilesApi", new { id = 0 }); ;
            string userId = request.userName;
            IResultServiceModel<ProjectFilesListResponse> response = new ResultServiceModel<ProjectFilesListResponse>();

            try
            {
                var rmsList = await this.customerFileService.GetCustomerFilesByProjectAsync(request.userName, request.projectId);

                if (rmsList.Success)
                {
                    response.Value = new ProjectFilesListResponse();
                    response.Value.fileList = rmsList.Value;
                }
                else
                {
                    response.OnError(rmsList.ErrorMessage, rmsList.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }
            
            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectFilesListResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("History", Name = "FileHistoryApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> History([FromUri]FileHistoryRequest request)
        {
            IResultServiceModel<FileHistoryResponse> response = new ResultServiceModel<FileHistoryResponse>();

            try
            {
                var rmCredentials = this.ValidateUserCredentials(request.userName);
                if (rmCredentials.Success)
                {
                    response = await this.mainService.GetFileHistoryAsync(request.userName, request.id);               
                }
                else 
                {
                    response.OnError(rmCredentials);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);                
            }

            string uri = Url.Link("FileHistoryApi", new { id = request.id });
            var httpResponse = Request.CreateResponse<IResultServiceModel<FileHistoryResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);
            
            return httpResponse;
        }

        [Authorize]
        [Route("Item", Name = "FileItemApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> Item([FromUri]FileRequest request)
        {
            IResultServiceModel<FileResponse> response = new ResultServiceModel<FileResponse>();

            try
            {
                var rmCredentials = this.ValidateUserCredentials(request.userName);
                if (rmCredentials.Success)
                {
                    response = await this.mainService.GetFileAsync(request.id, request.userName, request.projectId);
                }
                else
                {
                    response.OnError(rmCredentials);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            string uri = Url.Link("FileItemApi", new { id = request.id });
            var httpResponse = Request.CreateResponse<IResultServiceModel<FileResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


        [Authorize]
        [Route("AddCommentToState", Name = "FileAddCommentToStateApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> AddCommentToState(FileStateCommentRequest request)
        {
            IResultServiceModel<FileStateCommentResponse> response = new ResultServiceModel<FileStateCommentResponse>();

            try
            {
                var rmCredentials = this.ValidateUserCredentials(request.userName);
                if (rmCredentials.Success)
                {
                    response = await this.mainService.AddCommentToFileStateAsync(request);
                }
                else
                {
                    response.OnError(rmCredentials);
                }
            }
            catch (Exception ex)
            {
                response.OnException(ex);                
            }

            string uri = Url.Link("FileAddCommentToStateApi", new { id = request.id });
            var httpResponse = Request.CreateResponse<IResultServiceModel<FileStateCommentResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("List", Name = "FilesListApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> List(int id)
        {
            var httpResponse = Request.CreateResponse<string>(HttpStatusCode.Created, null);
            return await Task.FromResult(httpResponse);
        }



    }
}
