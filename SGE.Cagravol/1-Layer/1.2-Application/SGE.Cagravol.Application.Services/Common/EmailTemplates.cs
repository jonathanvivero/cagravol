using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using SGE.Cagravol.Presentation.Resources.Templates;

namespace SGE.Cagravol.Application.Services.Common
{
    public class EmailTemplateService
        : IEmailTemplateService
    {
        public string CustomerSignUp(string emailAddress, string loginUrl = "")
        {            
            string template = EmailTemplateResources.CustomerSignUp.sf(emailAddress, loginUrl);

            return template;
        }

        public string TestMessage(string message)
        {
            return message;
        }


        public string CustomerAssigned(string emailAddress, string pwd, string loginUrl = "")
        {

            string template = EmailTemplateResources.CustomerAssigned.sf(emailAddress, pwd, loginUrl);

            return template;
        }

        public string CustomerReserved(string emailAddress, string loginUrl = "")
        {

            string template = EmailTemplateResources.CustomerReserved.sf(emailAddress, loginUrl);

            return template;
        }
    }
}
