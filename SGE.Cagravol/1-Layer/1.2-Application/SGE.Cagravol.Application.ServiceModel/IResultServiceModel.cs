using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.ServiceModel
{   
    public interface IResultServiceModel<TResult>
    {
        bool Success { get; set; }
        string Message { get;set; }
        string ErrorMessage { get; set; }
        string ErrorCode { get; set; }
        TResult Value { get; set; }
        Exception Exception { get; set; }
        IResultServiceModel<TResult> OnException(Exception ex, string errorCode = "");
        IResultServiceModel<TResult> OnError(string errorMessage = "", string errorCode = "");
        IResultServiceModel<TResult> OnError(IResultModel rm);
        IResultServiceModel<TResult> OnSuccess(TResult value, string message = "");
        IResultModel ToResultModel();
    }
}
