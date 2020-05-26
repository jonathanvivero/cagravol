using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Mailing
{
    public interface IMailingService
    {
        IResultModel SendEmailNewCustomerSignUp(Customer customer, string hostAddress);
        IResultModel SendTestEmail(string email, string message);
        IResultModel SendReportToUsers(string subject, string[] listOfEmails, string message);
        IResultModel SendEmailSpaceRegisteredForCustomer(Customer customer, string hostAddress, string cuzEmail, string password);
        IResultModel SendEmailSpaceReservedForCustomer(Customer customer, string hostAddress, string cuzEmail);
    }
}
