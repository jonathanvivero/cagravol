using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Application.Services.Common;
using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using SGE.Cagravol.Presentation.Resources.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Mailing
{
    public class MailingService 
        : IMailingService
    {
        private readonly IEmailService emailService;
        private readonly IEmailTemplateService emailTemplateService;
        
        public MailingService(IEmailService emailService,
            IEmailTemplateService emailTemplateService) 
        {
            this.emailService = emailService;
            this.emailTemplateService = emailTemplateService;            
        }

        public IResultModel SendEmailNewCustomerSignUp(Customer customer, string hostAddress)
        {
            IResultModel rm = new ResultModel();
            EmailMessageServiceModel em = new EmailMessageServiceModel();
            //hostAddress += "#/login";
            hostAddress += "#/login/{0}".sf(customer.Email);

            try
            {
                em.Body = this.emailTemplateService.CustomerSignUp(customer.Email, hostAddress);
                em.DontReplyThisMessage = true;
                em.IsHtml = true;
                em.Subject = CustomerResources.SignUpMailSubject;
                em.TargetUserEmail = customer.Email;

                var rmEmail = this.emailService.SendMail(em);
                if (rmEmail.Success)
                {
                    rm.OnSuccess(rmEmail.Message);
                }
                else
                {
                    rm.OnError(rmEmail.ErrorMessage, rmEmail.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        public IResultModel SendTestEmail(string email, string message)
        {
            IResultModel rm = new ResultModel();
            EmailMessageServiceModel em = new EmailMessageServiceModel();

            try
            {
                em.Body = this.emailTemplateService.TestMessage(message);
                em.DontReplyThisMessage = true;
                em.IsHtml = true;
                em.Subject = "[GRAFIDEC] Email de Prueba";
                em.TargetUserEmail = email;

                var rmEmail = this.emailService.SendMail(em);
                if (rmEmail.Success)
                {
                    rm.OnSuccess(rmEmail.Message);
                }
                else
                {
                    rm.OnError(rmEmail.ErrorMessage, rmEmail.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        public IResultModel SendReportToUsers(string subject, string[] listOfEmails, string message)
        {
            IResultModel rm = new ResultModel();
            IResultModel rmEmail;
            EmailMessageServiceModel em = new EmailMessageServiceModel();

            try
            {
                em.Body = message;
                em.DontReplyThisMessage = true;
                em.IsHtml = true;
                em.Subject = subject;

                em.SendToSeveral = true;
                em.UserEmailList = new List<string>(listOfEmails);

                rmEmail = this.emailService.SendMailToSeveral(em);

                if (!rmEmail.Success)
                {
                    return rm.OnError(rmEmail);
                }
                else 
                {
                    rm.OnSuccess();                
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public IResultModel SendEmailSpaceRegisteredForCustomer(Customer customer, string hostAddress, string cuzEmail, string password)
        {
            IResultModel rm = new ResultModel();
            EmailMessageServiceModel em = new EmailMessageServiceModel();
            hostAddress += "#/login/{0}".sf(cuzEmail);

            try
            {
                em.Body = this.emailTemplateService.CustomerAssigned(customer.Email, hostAddress, password);
                em.DontReplyThisMessage = true;
                em.IsHtml = true;
                em.Subject = CustomerResources.SignUpMailSubject;
                em.TargetUserEmail = customer.Email;

                var rmEmail = this.emailService.SendMail(em);
                if (rmEmail.Success)
                {
                    rm.OnSuccess(rmEmail.Message);
                }
                else
                {
                    rm.OnError(rmEmail.ErrorMessage, rmEmail.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }

        public IResultModel SendEmailSpaceReservedForCustomer(Customer customer, string hostAddress, string cuzEmail)
        {
            IResultModel rm = new ResultModel();
            EmailMessageServiceModel em = new EmailMessageServiceModel();
            hostAddress += "#/rsignup/{0}".sf(customer.Id);

            try
            {
                em.Body = this.emailTemplateService.CustomerReserved(customer.Email, hostAddress);
                em.DontReplyThisMessage = true;
                em.IsHtml = true;
                em.Subject = CustomerResources.ReservationMailSubject;
                em.TargetUserEmail = customer.Email;

                var rmEmail = this.emailService.SendMail(em);
                if (rmEmail.Success)
                {
                    rm.OnSuccess(rmEmail.Message);
                }
                else
                {
                    rm.OnError(rmEmail.ErrorMessage, rmEmail.ErrorCode);
                }
            }
            catch (Exception ex)
            {
                rm.OnException(ex);
            }

            return rm;
        }
        
    }
}
