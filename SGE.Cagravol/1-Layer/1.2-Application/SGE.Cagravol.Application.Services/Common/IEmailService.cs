using Microsoft.AspNet.Identity;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
	public interface IEmailService
	{
		/// <summary>
		/// Sends the mail.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		void SendMail(string email, string subject, string body);
		IResultModel SendMail(EmailMessageServiceModel em);
        IResultModel SendMailToSeveral(EmailMessageServiceModel em);
		Task SendAsync(IdentityMessage message);		
	}
}
