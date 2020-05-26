using SGE.Cagravol.Application.Services.WebApi;
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
    [RoutePrefix("api/Test")]
    [EnableCors(origins: "http://localhost:2621, *", headers: "* ", methods: "* ")]
    public class TestController : BaseApiController
    {
        //private string LogType;
        //private string LogTypeResponse;

        public TestController(IWebApiContentService mainService)
            : base(mainService)
        {
            //this.LogType = "Test";
            //this.LogTypeResponse = "TestResponse";		    
        }

        [AllowAnonymous]
        [Route("", Name="TestApi")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage httpResponse = null;
            string uri = string.Empty;
            IResultModel response = await Task.FromResult(new ResultModel()
            {
                Success = true,
                Message = "This is a GET Test with no parameters."
            });

            httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
            uri = Url.Link("TestApi", new { id = 0 });

            httpResponse.Headers.Location = new Uri(uri);

            return httpResponse;
        }
    }
}
