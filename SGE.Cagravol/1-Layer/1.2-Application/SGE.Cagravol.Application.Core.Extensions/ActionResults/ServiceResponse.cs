using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Extensions.ActionResults
{
    public class ServiceResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public object JsonData { get; set; }
        public string PartialView { get; set; }
        public string PartialViewAlt { get; set; }

        public ServiceResponse()
        {
            this.Error = false;
        }
    }
}
