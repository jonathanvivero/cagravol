using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using SGE.Cagravol.Presentation.Resources.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.ServiceModel;

namespace SGE.Cagravol.Application.Services.Common
{
	
	//public sealed class EmailService : IEmailService
	public sealed class EmailService : IIdentityMessageService, IEmailService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your email service here to send an email.
			return Task.FromResult(0);			
		}

		/// <summary>
		/// The SMTP address.
		/// </summary>
		private readonly string smtp;

		/// <summary>
		/// The sender email direction.
		/// </summary>
		private readonly string systemEmailUser;

		/// <summary>
		/// The sender account email password.
		/// </summary>
		private readonly string systemEmailPassword;

		/// <summary>
		/// Initializes a new instance of the <see cref="EmailService" /> class.
		/// </summary>
		public EmailService()
		{
			this.smtp = ConfigurationManager.AppSettings["email.smtpaddress"];
			this.systemEmailUser = ConfigurationManager.AppSettings["email.emailUser"];
			this.systemEmailPassword = ConfigurationManager.AppSettings["email.emailPassword"];
		}

		/// <summary>
		/// Sends the mail.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="email">The email.</param>
		/// <param name="subject">The subject.</param>
		/// <param name="body">The body.</param>
		public void SendMail(string email, string subject, string body)
		{
			var message = new System.Net.Mail.MailMessage();
			MailAddress userEmail = new MailAddress(email);

			MailAddress systemEmail = new MailAddress(this.systemEmailUser);
			message.IsBodyHtml = true;
			message.Sender = systemEmail;
			message.From = systemEmail;
			message.Headers.Add("Reply-To", this.systemEmailUser);
			message.To.Add(userEmail);
			message.Subject = subject;
			message.Body = body;
			var smtp = new System.Net.Mail.SmtpClient(this.smtp, Convert.ToInt32(25));
			smtp.UseDefaultCredentials = true;
			var credentials = new NetworkCredential(this.systemEmailUser, this.systemEmailPassword);
			smtp.Credentials = credentials;
			smtp.EnableSsl = false;

			//TODO: Transient fault!!
			smtp.Send(message);
		}

		public IResultModel SendMail(EmailMessageServiceModel em)
		{

            var response = new ResultModel();

			try
			{
				var message = new System.Net.Mail.MailMessage();
				MailAddress userEmail = new MailAddress(em.TargetUserEmail);
				MailAddress systemEmail = new MailAddress(this.systemEmailUser);
				message.IsBodyHtml = em.IsHtml;
				//message.Sender = systemEmail;
				message.To.Add(userEmail);
				message.Subject = em.Subject;
				message.Body = em.Body;

				if (!string.IsNullOrEmpty(em.FromEmail))
				{
					message.From = new MailAddress(em.FromEmail);
				}
				else 
				{
					message.From = systemEmail;
				}
				if (!string.IsNullOrWhiteSpace(em.ReplyToEmail))
				{
					message.ReplyToList.Add(new MailAddress(em.ReplyToEmail));
					//message.Headers.Add("Reply-To", em.ReplyToEmail);
				}
			
				var smtp = new System.Net.Mail.SmtpClient(this.smtp, Convert.ToInt32(25));
				smtp.UseDefaultCredentials = true;
				var credentials = new NetworkCredential(this.systemEmailUser, this.systemEmailPassword);
				smtp.Credentials = credentials;
				smtp.EnableSsl = false;

				//TODO: Transient fault!!
				smtp.Send(message);

				response.Success = true;				
			}
			catch (Exception ex) 
			{
				response.Success = false;
				response.Exception = ex;
				response.Message = ErrorResources.ErrorAtSendingEmailMessage;
			}

			return response;		
		}

        public IResultModel SendMailToSeveral(EmailMessageServiceModel em)
        {

            var response = new ResultModel();

            if (!em.SendToSeveral)
                return response.OnError();

            try
            {
                var message = new System.Net.Mail.MailMessage();
                MailAddress userEmail;
                MailAddress systemEmail = new MailAddress(this.systemEmailUser);
                message.IsBodyHtml = em.IsHtml;
                //message.Sender = systemEmail;

                foreach (string address in em.UserEmailList)
                {
                    userEmail = new MailAddress(address);
                    message.To.Add(userEmail);
                }
                message.Subject = em.Subject;
                message.Body = em.Body;

                if (!string.IsNullOrEmpty(em.FromEmail))
                {
                    message.From = new MailAddress(em.FromEmail);
                }
                else
                {
                    message.From = systemEmail;
                }
                if (!string.IsNullOrWhiteSpace(em.ReplyToEmail))
                {
                    message.ReplyToList.Add(new MailAddress(em.ReplyToEmail));
                    //message.Headers.Add("Reply-To", em.ReplyToEmail);
                }

                var smtp = new System.Net.Mail.SmtpClient(this.smtp, Convert.ToInt32(25));
                smtp.UseDefaultCredentials = true;
                var credentials = new NetworkCredential(this.systemEmailUser, this.systemEmailPassword);
                smtp.Credentials = credentials;
                smtp.EnableSsl = false;

                //TODO: Transient fault!!
                smtp.Send(message);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Exception = ex;
                response.Message = ErrorResources.ErrorAtSendingEmailMessage;
            }

            return response;


        }
		
	}
}
