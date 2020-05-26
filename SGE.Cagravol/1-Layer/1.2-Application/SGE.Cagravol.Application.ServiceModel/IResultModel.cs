using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.ServiceModel
{
    public interface IResultModel
    {
        bool Success { get; set; }
        string Message { get;set; }
        string ErrorMessage { get; set; }
        string ErrorCode { get; set; }
        Exception Exception { get; set; }
        IResultModel OnException(Exception ex, string errorCode = "");
        IResultModel OnError(IResultModel rm);
        IResultModel OnError(string errorMessage = "", string errorCode = "");
        IResultModel OnSuccess(string message = "");

        IResultModelJSON ToJSONModel();
    }
}
