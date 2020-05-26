using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Services.Common
{
	public interface IEmailManagerService
	{
		void SendEmailToUser<T>(T emailViewModel, string senderEmail, Controller controller)
		   where T : BaseEmailServiceModel;

		/// <summary>
		/// Gets the email types.
		/// </summary>
		/// <returns>A list of available type emails.</returns>
		IEnumerable<string> GetEmailTypes();


		/// <summary>
		/// Gets the type of the email view model by.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		BaseEmailServiceModel GetEmailViewModelByType(string type);
	}
}
