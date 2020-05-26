using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.ServiceModel
{
    public class ResultServiceModel<TResult> : IResultServiceModel<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public TResult Value { get; set; }
        public string ErrorCode { get; set; }

        public ResultServiceModel()
        {
            this.Value = default(TResult);
            this.Success = false;
            this.ErrorMessage = string.Empty;
            this.Message = string.Empty;
            this.Exception = null;
            this.ErrorMessage = string.Empty;
        }

        public ResultServiceModel(bool success = false)
            : this()
        {
            this.Success = success;
            this.ErrorMessage = string.Empty;
        }
        public ResultServiceModel(TResult defaultValue)
            : this()
        {
            this.Value = defaultValue;
            this.ErrorMessage = string.Empty;
        }

        public IResultServiceModel<TResult> OnException(Exception ex, string errorCode = "")
        {
            this.Exception = ex;
            this.ErrorMessage = ex.Message;
            this.Success = false;
            this.Value = default(TResult);
            this.ErrorCode = errorCode;
            return this;
        }

        public IResultServiceModel<TResult> OnError(string errorMessage = "", string errorCode = "")
        {
            this.Success = false;
            this.ErrorMessage = errorMessage;
            this.Value = default(TResult);
            this.ErrorCode = errorCode;
            return this;
        }
        public IResultServiceModel<TResult> OnError(IResultModel rm)
        {
            this.Success = false;
            this.ErrorMessage = rm.ErrorMessage;
            this.Value = default(TResult);
            this.ErrorCode = rm.ErrorCode;
            return this;
        }        

        public IResultServiceModel<TResult> OnSuccess(TResult value, string message = "")
        {
            this.Success = true;
            this.Message = message;
            this.Value = value;
            this.ErrorCode = string.Empty;
            return this;
        }

        public IResultModel ToResultModel()
        {
            IResultModel rm = new ResultModel()
            {
                ErrorMessage = this.ErrorMessage,
                ErrorCode = this.ErrorCode,
                Exception = this.Exception,
                Message = this.Message,
                Success = this.Success
            };

            return rm;
        }

    }
}
