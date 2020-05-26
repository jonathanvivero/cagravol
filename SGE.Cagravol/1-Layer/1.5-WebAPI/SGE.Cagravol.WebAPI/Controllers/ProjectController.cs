using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Services.Projects;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.Entities.Projects;
using SGE.Cagravol.Domain.JSON.Projects;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Presentation.Resources.Common;
using SGE.Cagravol.Presentation.Resources.Projects;
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
using SGE.Cagravol.Domain.POCO.Projects;
using SGE.Cagravol.Domain.JSON.Customers;
using SGE.Cagravol.Application.Services.Customers;
using SGE.Cagravol.Domain.POCO.Customers;
using SGE.Cagravol.Presentation.Resources.Customers;
using SGE.Cagravol.Domain.POCO.Files;
using SGE.Cagravol.Presentation.Resources.Files;
using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Domain.Entities.Common;
using System.IO;
using System.Net.Http.Headers;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Project")]
    public class ProjectController
        : BaseApiController
    {

        private readonly IProjectService projectService;
        protected readonly ICustomerService customerService;
        protected readonly IMiscService miscService;

        public ProjectController(IWebApiContentService mainService,
            IProjectService projectService,
            ICustomerService customerService,
            IMiscService miscService)
            : base(mainService)
        {
            this.projectService = projectService;
            this.customerService = customerService;
            this.RaiseErrorMessageEvent += this.GetErrorMessageEvent;
            this.miscService = miscService;
        }

        protected void GetErrorMessageEvent(object sender, ErrorMessageEventArgs e)
        {
            switch (e.Code)
            {
                case "request.extraChargePercentage":
                    e.Message = ProjectResources.extraChargePercentageShoudBeAPercentage;
                    break;
                default:
                    e.Message = string.Empty;
                    break;
            }

        }

        [Authorize]
        [Route("", Name = "ProjectApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get([FromUri]ProjectListRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectApi", new { id = 0 }); ;
            string userId = request.userName;
            IResultServiceModel<ProjectListResponse> response = new ResultServiceModel<ProjectListResponse>();

            try
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    var rmsList = await this.projectService.GetListByUserAsync(request.userName);

                    if (rmsList.Success)
                    {
                        response.OnSuccess(new ProjectListResponse()
                        {
                            list = rmsList.Value.Select(s => new ProjectPOCO(s))
                        });
                    }
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectListResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("Item", Name = "ProjectItemApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetItem([FromUri]ProjectItemRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectItemApi", new { id = request.id }); ;
            string userId = request.userName;
            IResultServiceModel<ProjectItemResponse> response = new ResultServiceModel<ProjectItemResponse>();

            try
            {

                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    var rmsItem = await this.projectService.GetProjectByIdAndUserAsync(request.userName, request.id);

                    if (rmsItem.Success)
                    {
                        response.OnSuccess(new ProjectItemResponse()
                        {
                            item = new ProjectPOCO(rmsItem.Value)
                        });
                    }
                    else
                    {
                        response.OnError(rmsItem.ErrorMessage, rmsItem.ErrorCode);
                    }
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectItemResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("", Name = "ProjectPostApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostItem([FromBody]ProjectItemRequest request)
        {

            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectPostApi", new { id = request.id });
            string userId = request.userName;
            IResultServiceModel<ProjectItemResponse> response = new ResultServiceModel<ProjectItemResponse>();

            if (!ModelState.IsValid)
            {
                response.OnError(this.GetErrorsFromModelState(), EnumErrorCode.VALIDATION_ERROR);
            }
            else
            {
                try
                {

                    var rmCred = this.ValidateUserCredentials(request.userName);
                    if (rmCred.Success)
                    {
                        IResultServiceModel<Project> rmsItem;

                        if (request.id == 0)
                        {
                            rmsItem = await this.projectService.AddProjectByUserAsync(request);
                        }
                        else
                        {
                            rmsItem = await this.projectService.EditProjectByUserAsync(request);
                        }

                        if (rmsItem.Success)
                        {
                            response.OnSuccess(new ProjectItemResponse()
                            {
                                item = new ProjectPOCO(rmsItem.Value)
                            });
                        }
                        else
                        {
                            response.OnError(
                                ErrorResources.ModelStateErrors_Format.sf(rmsItem.ErrorMessage),
                                rmsItem.ErrorCode
                            );
                        }
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectItemResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


        [Authorize]
        [Route("Delete", Name = "ProjectDeleteApi")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteItem([FromUri]ProjectItemRequest request)
        {

            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectDeleteApi", new { id = request.id });
            string userId = request.userName;
            IResultServiceModel<ProjectItemResponse> response = new ResultServiceModel<ProjectItemResponse>();

            if (request.id <= 0)
            {
                response.OnError(ProjectResources.ErrorItemDoesNotExist_OnDelete, EnumErrorCode.ITEM_DOES_NOT_EXIST);
            }
            else
            {
                try
                {
                    var rmCred = this.ValidateUserCredentials(request.userName);
                    if (rmCred.Success)
                    {
                        IResultModel rmDelete = await this.projectService.DeleteProjectByUserAsync(request);

                        if (rmDelete.Success)
                        {
                            response.OnSuccess(new ProjectItemResponse()
                            {
                                item = new ProjectPOCO() { Id = request.id }
                            });
                        }
                        else
                        {
                            response.OnError(
                                ErrorResources.ModelStateErrors_Format.sf(rmDelete.ErrorMessage),
                                rmDelete.ErrorCode
                            );
                        }
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectItemResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


        [Authorize]
        [Route("CustomerActivity", Name = "ProjectCustomerActivityApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> CustomerActivity([FromUri]ProjectCustomerActivityRequest request)
        {

            IResultServiceModel<ProjectCustomerActivityResponse> response = new ResultServiceModel<ProjectCustomerActivityResponse>();

            var rmCred = this.ValidateUserCredentials(request.userName);
            if (rmCred.Success)
            {
                var cuz = this.mainService.GetCustomer(request.pId, request.id, request.userName);
                response.OnSuccess(
                    new ProjectCustomerActivityResponse()
                    {
                        Customer = cuz.Value
                    }
                );
            }
            else
            {
                response.OnError(rmCred);
            }

            var httpResponse = Request.CreateResponse<IResultServiceModel<ProjectCustomerActivityResponse>>(HttpStatusCode.Created, response);
            return await Task.FromResult(httpResponse);


            //IResultServiceModel<ProjectCustomerActivityResponse> response = new ResultServiceModel<ProjectCustomerActivityResponse>();

            //var rmCred = this.ValidateUserCredentials(request.userName);
            //if (rmCred.Success)
            //{
            //    var cuz = this.mainService.GetCustomerForProjectGeneralSpace(request.id, request.userName);
            //    response.OnSuccess(
            //        new ProjectCustomerActivityResponse()
            //        {
            //            Customer = new CustomerPOCO(cuz.Value)
            //        }
            //    );
            //}
            //else
            //{
            //    response.OnError(rmCred);
            //}

            //var httpResponse = Request.CreateResponse<IResultServiceModel<ProjectCustomerActivityResponse>>(HttpStatusCode.Created, response);
            //return await Task.FromResult(httpResponse);
        }


        [Authorize]
        [Route("GSActivity", Name = "ProjectGSActivityApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> GSActivity([FromUri]ProjectGSpaceActivityRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectGSActivityApi", new { userName = request.userName }); ;
            string userId = request.userName;
            IResultServiceModel<ProjectGSpaceActivityResponse> response = new ResultServiceModel<ProjectGSpaceActivityResponse>();

            try
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    var rsmCuz = this.mainService.GetCustomerForProjectGeneralSpace(request.pId, request.userName);
                    var cuz = new CustomerPOCO(rsmCuz.Value);
                    response.OnSuccess(new ProjectGSpaceActivityResponse()
                    {
                        list = cuz.Files
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectGSpaceActivityResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


        [Authorize]
        [Route("GSItem", Name = "ProjectGSItemApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> GSItem([FromUri]ProjectGSpaceItemRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectGSItemApi", new { userName = request.userName }); ;
            string userId = request.userName;
            IResultServiceModel<ProjectGSpaceItemResponse> response = new ResultServiceModel<ProjectGSpaceItemResponse>();

            try
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    var rsmFile = this.mainService.GetCustomerFileForProjectGeneralSpace(request.id, request.projectId, request.userName);

                    var file = new FilePOCO(rsmFile.Value);

                    response.OnSuccess(new ProjectGSpaceItemResponse()
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectGSpaceItemResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [Authorize]
        [Route("GSDelete", Name = "ProjectGSDeleteApi")]
        [HttpDelete]
        public async Task<HttpResponseMessage> GSDelete([FromUri]CustomerFileDeleteRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectGSDeleteApi", new { id = request.id });
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
                        response = await this.customerService.DeleteGeneralSpaceFileAsync(request);
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
        [Route("GSUpdateItem", Name = "ProjectGSUpdateItemApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> GSUpdateItem([FromBody]ProjectGSpaceUpdateItemRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectGSUpdateItemApi", new { userName = request.userName }); ;
            string userId = request.userName;
            IResultModel response = new ResultModel();

            try
            {
                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    if (request.fileTypeId <= 0)
                    {
                        response.OnError(FileResources.ErrorFileTypeCannotBeUnknown);
                    }
                    else if (string.IsNullOrWhiteSpace(request.name))
                    {
                        response.OnError(FileResources.ErrorFileNameCannotBeEmpty);
                    }
                    else
                    {
                        response = this.mainService.UpdateFileForProjectGeneralSpace(request.id, request.name, request.fileTypeId, request.projectId, request.userName);
                    }

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


        [Authorize]
        [Route("Excel", Name = "ProjectExcelApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> Excel([FromBody]ProjectExcelRequest request)
        {

            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectExcelApi", new { id = request.id });
            string userId = request.userName;
            IResultServiceModel<ProjectExcelResponse> response = new ResultServiceModel<ProjectExcelResponse>();

            try
            {

                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    IResultServiceModel<string> rmsGUID;

                    rmsGUID = this.miscService.GenerateDownloadExcelFileId(request.id);

                    if (rmsGUID.Success)
                    {
                        var host = this.GetBaseUrl();
                        host = "{0}api/Project/DownloadExcel?guid={1}".sf(host, rmsGUID.Value);

                        var resp = new ProjectExcelResponse()
                        {
                            url = host
                        };

                        response.OnSuccess(resp);
                    }
                    else
                    {
                        response.OnError(rmsGUID.ErrorMessage, rmsGUID.ErrorCode);
                    }                   
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


            httpResponse = Request.CreateResponse<IResultServiceModel<ProjectExcelResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [AllowAnonymous]
        [Route("DownloadExcel", Name = "ProjectDownloadExcelApi")]
        [HttpGet]
        public HttpResponseMessage DownloadExcel(string guid)
        {
            string path = string.Empty;            
            long projectId = -1;

            try
            {
                var rmExcelGuid = this.miscService.CheckDownloadExcelFileId(guid);

                if (rmExcelGuid.Success)
                {
                    projectId = rmExcelGuid.Value;                    
                }


                MemoryStream content;
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                var rsm = this.projectService.GenerateExcel(projectId);

                if (rsm.Success)
                {
                    content = rsm.Value;

                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    //response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                    //Byte[] bytes = File.ReadAllBytes(path);
                    //String file = Convert.ToBase64String(bytes);

                    response.Content = new StreamContent(content); //new ByteArrayContent(b); //
                    
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    response.Content.Headers.ContentDisposition.FileName = "Event_Activity_{0}.xlsx".sf(DateTime.Now.ToShortDateString());


                    this.miscService.RemoveExcelGUID(guid);
                }
                else 
                {
                    response = new HttpResponseMessage(HttpStatusCode.NoContent);
                }

                return response;

            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
                       

            //HttpResponseMessage httpResponse = null;
            //string uri = Url.Link("ProjectDownloadExcelApi", new { guid = guid }); ;
            //IResultServiceModel<string> response = new ResultServiceModel<string>();

            //try
            //{
            //    response.OnSuccess(guid);
            //}
            //catch (Exception ex)
            //{
            //    response.OnException(ex);
            //}

            //httpResponse = Request.CreateResponse<IResultServiceModel<string>>(HttpStatusCode.Created, response);
            //httpResponse.Headers.Location = new Uri(uri);

            //return httpResponse;
        }


        [Authorize]
        [Route("ChangeSpaceStatus", Name = "ProjectChangeSpaceStatusApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> ChangeSpaceStatus([FromBody]ChangeSpaceStatusRequest request)
        {

            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("ProjectChangeSpaceStatusApi", new { customerId = request.customerId, currentStatus = request.currentStatus, email = request.email, index = request.index, newStatus= request.newStatus, password = request.password, projectId = request.projectId, registered = request.registered, userName = request.userName });

            string userId = request.userName;
            IResultServiceModel<ChangeSpaceStatusResponse> response = new ResultServiceModel<ChangeSpaceStatusResponse>();

            if (!ModelState.IsValid)
            {
                response.OnError(this.GetErrorsFromModelState(), EnumErrorCode.VALIDATION_ERROR);
            }
            else
            {
                try
                {

                    var rmCred = this.ValidateUserCredentials(request.userName);
                    if (rmCred.Success)
                    {
                        response = await this.mainService.ChangeCustomerSpaceStatus(request, this.GetBaseUrl());                                                
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

            httpResponse = Request.CreateResponse<IResultServiceModel<ChangeSpaceStatusResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }



    }
}
