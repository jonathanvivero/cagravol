using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.ServiceModel
{
    public class ResultModel : IResultModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public string ErrorCode { get; set; }
        public ResultModel()
        {
            this.Success = false;
            this.ErrorMessage = string.Empty;
            this.ErrorCode = string.Empty;
            this.Message = string.Empty;
            this.Exception = null;
        }

        public ResultModel(bool success = false)
            : this()
        {
            this.Success = success;            
        }        

        public IResultModel OnException(Exception ex, string errorCode = "")
        {
            this.Exception = ex;
            this.ErrorMessage = ex.Message;
            this.Success = false;
            this.ErrorCode = errorCode;
            return this;
        }

        public IResultModel OnError(string errorMessage = "", string errorCode = "")
        {
            this.Success = false;
            this.ErrorMessage = errorMessage;
            this.ErrorCode = errorCode;
            return this;
        }
        public IResultModel OnError(IResultModel rm)
        {
            this.Success = false;
            this.ErrorMessage = rm.ErrorMessage;
            this.ErrorCode = rm.ErrorCode;
            return this;
        }
        public IResultModel OnSuccess(string message = "")
        {
            this.Success = true;
            this.Message = message;
            this.ErrorCode = string.Empty;
            return this;
        }

        public IResultModelJSON ToJSONModel()
        {
            return new ResultModelJSON()
            {
                errorCode = this.ErrorCode,
                message = this.Message,
                errorMessage = this.ErrorMessage,
                success = this.Success
            };
        }
    }
}
