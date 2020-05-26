using SGE.Cagravol.Application.Services.Panels;
using SGE.Cagravol.Application.Services.WebApi;
using SGE.Cagravol.Domain.JSON.Panels;
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
    [RoutePrefix("api/Panels")]
    [EnableCors(origins: "*", headers: "* ", methods: "* ")]
    public class PanelsController : BaseApiController
    {

        private readonly IPanelService panelService;

        public PanelsController(IWebApiContentService mainService,
            IPanelService panelService)
            : base(mainService)
        {
            this.panelService = panelService;
        }

        // GET api/<controller>
        [Authorize]
        public async Task<HttpResponseMessage> Get([FromUri]PanelConfigurationRequest request)
        {
            HttpResponseMessage httpResponse = null;
            string uri = Url.Link("DefaultApi", new { id = 0 }); ;
            string userId = request.userName;
            IResultServiceModel<PanelConfigurationResponse> response;

            try
            {
                response = await this.panelService.GetConfigurationCurrentUserAsync(userId);

            }
            catch (Exception ex)
            {
                response = new ResultServiceModel<PanelConfigurationResponse>();
                response.OnException(ex);
            }

            httpResponse = Request.CreateResponse<IResultServiceModel<PanelConfigurationResponse>>(HttpStatusCode.Created, response);
            httpResponse.Headers.Location = new Uri(uri);
            return httpResponse;
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}