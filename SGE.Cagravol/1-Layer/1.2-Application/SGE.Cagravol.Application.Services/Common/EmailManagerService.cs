using SGE.Cagravol.Application.Core.Helpers;
using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using SGE.Cagravol.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Services.Common
{
	/// <summary>
	/// Email manager service. Use this manager to send emails.
	/// </summary>
	public sealed class EmailManagerService : IEmailManagerService
	{
		private readonly IEmailFactoryService emailFactoryService;
		private readonly IEmailService emailService;

		public EmailManagerService(IEmailFactoryService emailFactoryService,
			IEmailService emailService)
		{
			Check.NotNull<IEmailFactoryService>(emailFactoryService, "emailFactoryService");
			Check.NotNull<IEmailService>(emailService, "emailService");

			this.emailFactoryService = emailFactoryService;
			this.emailService = emailService;
		}

		/// <summary>
		/// Sends the email to user.
		/// </summary>
		/// <typeparam name="T">A BaseViewModel class</typeparam>
		/// <param name="emailViewModel">The email view model.</param>
		/// <param name="senderEmail">The sender email.</param>
		/// <param name="controller">The controller.</param>
		public void SendEmailToUser<T>(T emailViewModel, string senderEmail, Controller controller)
			where T : BaseEmailServiceModel
		{
			try
			{
				var emailHtml = this.emailFactoryService.GetEmail<T>(controller, emailViewModel);

				this.emailService.SendMail(senderEmail, emailHtml.Subject, emailHtml.Body);
			}
			catch (Exception ex)
			{
				Trace.Write(ex);
				throw ex;
			}
		}


		/// <summary>
		/// Gets the email types.
		/// </summary>
		/// <returns>A list of available type emails.</returns>
		public IEnumerable<string> GetEmailTypes()
		{
			var emailViewModels = AssemblyHelpers
                .GetViewModelTypes<BaseEmailServiceModel>()
                .Where(i => i.Name != "BaseEmailServiceModel")
				.Select(i => i.Name.Replace("EmailServiceModel", string.Empty));

			return emailViewModels;
		}

		/// <summary>
		/// Gets the type of the email view model by.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public BaseEmailServiceModel GetEmailViewModelByType(string type)
		{
			var typeName = type + "EmailServiceModel";
            var concreteObject = AssemblyHelpers.GetInstanceOf<BaseEmailServiceModel>(typeName);
			return concreteObject;
		}


	}
}
