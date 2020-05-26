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
using SGE.Cagravol.Application.Services.Common;
using System.IO;
using SGE.Cagravol.Application.Services.Projects;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace SGE.Cagravol.WebAPI.Controllers
{
    [RoutePrefix("api/Excel")]
    //[EnableCors(origins: "http://localhost:2621, *", headers: "* ", methods: "* ")]
    public class ExcelController : BaseApiController
    {
        //private string LogType;
        //private string LogTypeResponse;

        protected readonly IMiscService miscService;
        private readonly IProjectService projectService;
        public ExcelController(IWebApiContentService mainService,
            IMiscService miscService,
            IProjectService projectService)
            : base(mainService)
        {
            //this.LogType = "Test";
            //this.LogTypeResponse = "TestResponse";		    
            this.miscService = miscService;
            this.projectService = projectService;
        }

        //[AllowAnonymous]
        //[Route("", Name="ExcelApiNoParams")]
        //[HttpGet]
        //public async Task<HttpResponseMessage> Get()
        //{
        //    HttpResponseMessage httpResponse = null;
        //    string uri = string.Empty;
        //    IResultModel response = await Task.FromResult(new ResultModel()
        //    {
        //        Success = true,
        //        Message = "This is a GET Test with no parameters."
        //    });

        //    httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
        //    uri = Url.Link("TestApi", new { id = 0 });

        //    httpResponse.Headers.Location = new Uri(uri);

        //    return httpResponse;
        //}

        [AllowAnonymous]
        //[Route("", Name = "ExcelApi")]
        [ActionName("download")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {


            string path = string.Empty;
            long projectId = -1;

            try
            {
                var rmExcelGuid = this.miscService.CheckDownloadExcelFileId(id);

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


                    this.miscService.RemoveExcelGUID(id);
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
            //string uri = string.Empty;
            //IResultModel response = await Task.FromResult(new ResultModel()
            //{
            //    Success = true,
            //    Message = "This is a GET Test with no parameters."
            //});

            //httpResponse = Request.CreateResponse<IResultModel>(HttpStatusCode.Created, response);
            //uri = Url.Link("TestApi", new { id = 0 });

            //httpResponse.Headers.Location = new Uri(uri);

            //return httpResponse;
        }



        [AllowAnonymous]
        //[Route("", Name = "ExcelApi")]
        [ActionName("geta")]
        [HttpGet]
        public async Task<HttpResponseMessage> Geta(string id)
        {


            string path = string.Empty;
            long projectId = -1;

            try
            {
                var rmExcelGuid = this.miscService.CheckDownloadExcelFileId(id);

                if (rmExcelGuid.Success)
                {
                    projectId = rmExcelGuid.Value;
                }


                MemoryStream content;
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Found);

                var rsm = this.projectService.GenerateExcel(projectId);

                if (rsm.Success)
                {
                    content = rsm.Value;

                    response = new HttpResponseMessage(HttpStatusCode.OK);

                    response.Content = new StreamContent(content); //new ByteArrayContent(b); //

                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
                    response.Content.Headers.ContentDisposition.FileName = "Event_Activity_{0}.xlsx".sf(DateTime.Now.ToShortDateString());


                    this.miscService.RemoveExcelGUID(id);
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.NoContent);
                }

                return response;

            }
            catch (Exception ex)
            {
                HttpResponseMessage responseError = new HttpResponseMessage(HttpStatusCode.Conflict);                
                responseError.Content = new StringContent(ex.Message);
                return responseError; //new HttpResponseMessage(HttpStatusCode.Conflict);
            }            
        }
    }
}
