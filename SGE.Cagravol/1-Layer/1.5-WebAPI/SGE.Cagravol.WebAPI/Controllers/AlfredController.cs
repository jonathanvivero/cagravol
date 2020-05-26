using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.JSON.Alfred;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Alfred")]
    [EnableCors(origins: "* ", headers: "* ", methods: "* ")]
    public class AlfredController : BaseApiController
    {
        //private string LogType;
        //private string LogTypeResponse;

        public AlfredController(IWebApiContentService mainService)
            : base(mainService)
        {
            
        }

        [AllowAnonymous]
        [Route("DefaultPlatformParameters", Name="AlfredGetApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage httpResponse = null;
            string uri = string.Empty;
            IResultServiceModel<AlfredDefaultPlatformParametersResponse> response = new ResultServiceModel<AlfredDefaultPlatformParametersResponse>();
            Task<IResultServiceModel<AlfredDefaultPlatformParametersResponse>> taskResponse = null;

            try
            {
                taskResponse = this.mainService.GetDefaultPlatformParametersAsync();
            }
            catch (Exception ex)
            {
                response.OnException(ex);                
            }

            uri = Url.Link("AlfredGetApi", new { id = 0 });

            if (taskResponse != null) {
                response = await taskResponse;
            } else {
                response.OnSuccess(new AlfredDefaultPlatformParametersResponse() { publicKey = "", totalStands = 1 });
            }
            
            httpResponse = Request.CreateResponse<IResultServiceModel<AlfredDefaultPlatformParametersResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


        [AllowAnonymous]
        [Route("RecentProject", Name = "AlfredGetRecentProjectApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> RecentProject()
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("AlfredGetRecentProjectApi", new { id = 0 }); 
            IResultServiceModel<long> response = new ResultServiceModel<long>();            

            try
            {
                response = this.mainService.GetMostRecentProjectId();
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            httpResponse = Request.CreateResponse<IResultServiceModel<long>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }


        [AllowAnonymous]
        [Route("SendEmail", Name = "AlfredSendEmailApi")]
        [HttpGet]
        //public async Task<HttpResponseMessage> GetSendEmail([FromUri]SendTestEmailRequest request)
        public HttpResponseMessage GetSendEmail([FromUri]SendTestEmailRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = string.Empty;
            IResultModel response = new ResultModel();            
            
            try
            {
                response = this.mainService.SendTestEmail(request.email, request.message);
            }
            catch (Exception ex)
            {
                response.OnException(ex);
            }

            uri = Url.Link("AlfredSendEmailApi", new { id = 0 });

            httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }

        [AllowAnonymous]
        [Route("SpaceInfo", Name = "AlfredSpaceInfoApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> SpaceInfo([FromUri]SpaceInfoRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = string.Empty;
            IResultServiceModel<SpaceInfoResponse> rsm = new ResultServiceModel<SpaceInfoResponse>();
            SpaceInfoResponse response = null;

            try
            {
                var rmCuz = this.mainService.GetReservedCustomer(request.id);
                if (rmCuz.Success)
                {
                    response = new SpaceInfoResponse()
                    {
                        item = rmCuz.Value
                    };

                    rsm.OnSuccess(response);
                }
                else
                {
                    rsm.OnError(rmCuz.ErrorMessage, rmCuz.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rsm.OnException(ex);
            }

            uri = Url.Link("AlfredSpaceInfoApi", new { id = request.id });



            httpResponse = Request.CreateResponse<IResultServiceModel<SpaceInfoResponse>>(HttpStatusCode.Created, rsm);
            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }
    }
}
