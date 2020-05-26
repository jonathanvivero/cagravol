using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.ServiceModel
{
    public class ResultModelJSON : IResultModelJSON
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string errorMessage { get; set; }        
        public string errorCode { get; set; }
        public ResultModelJSON()
        {
            this.success = false;
            this.errorMessage = string.Empty;
            this.errorCode = string.Empty;
            this.message = string.Empty;
        }

        public ResultModelJSON(bool success = false)
            : this()
        {
            this.success = success;
        }      

        public IResultModelJSON onError(string errorMessage = "", string errorCode = "")
        {
            this.success = false;
            this.errorMessage = errorMessage;
            this.errorCode = errorCode;
            return this;
        }
        public IResultModelJSON onSuccess(string message = "")
        {
            this.success = true;
            this.message = message;
            this.errorCode = string.Empty;
            return this;
        }

        public IResultModel toMSModel()
        {
            return new ResultModel()
            {
                ErrorCode = this.errorCode,
                Message = this.message,
                ErrorMessage = this.errorMessage,
                Exception = null,
                Success = this.success
            };
        }
    }
}
