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
using SGE.Cagravol.Domain.JSON.Workflows;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Workflow")]
    public class WorkflowController
        : BaseApiController
    {

        private readonly ICustomerFileService customerFileService;

        public WorkflowController(IWebApiContentService mainService,
            ICustomerFileService customerFileService)
            : base(mainService)
        {
            this.customerFileService = customerFileService;
        }

        [Authorize]
        [Route("MoveFile", Name = "MoveFileApi")]
        [HttpPost]
        public async Task<HttpResponseMessage> MoveFile(WorkflowMoveFileRequest request)
        {   

            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("MoveFileApi", new { id = 0 }); ;
            string userId = request.userName;
            IResultServiceModel<WorkflowMoveFileResponse> response = new ResultServiceModel<WorkflowMoveFileResponse>();

            try
            {

                var rmCred = this.ValidateUserCredentials(request.userName);
                if (rmCred.Success)
                {
                    response = await this.mainService.MoveWorkflowStateForFileAsync(request);
                }
                else
                {
                    response.OnError(rmCred);
                }                
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            httpResponse = Request.CreateResponse<IResultServiceModel<WorkflowMoveFileResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

    }
}
