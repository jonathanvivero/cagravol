using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.ServiceModel
{
    public interface IResultModelJSON
    {
        bool success { get; set; }
        string message { get;set; }
        string errorMessage { get; set; }
        string errorCode { get; set; }
        IResultModelJSON onError(string errorMessage = "", string errorCode = "");
        IResultModelJSON onSuccess(string message = "");

        IResultModel toMSModel();
    }
}
