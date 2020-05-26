using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
    public interface IEmailTemplateService
    {
        string CustomerSignUp(string emailAddress, string loginUrl = "");
        string TestMessage(string message);
        string CustomerAssigned(string emailAddress, string pwd, string loginUrl = "");
        string CustomerReserved(string emailAddress, string loginUrl = "");
    }
}
