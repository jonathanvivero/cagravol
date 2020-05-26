using SGE.Cagravol.Domain.POCO.ServiceModels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Services.Common
{
	/// <summary>
	/// Interface to support Email Factory.
	/// </summary>
	public interface IEmailFactoryService
	{
		/// <summary>
		/// Gets the email.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller">The controller.</param>
		/// <param name="emailViewModel">The email view model.</param>
		/// <returns></returns>
		ResultEmailServiceModel GetEmail<T>(Controller controller, T emailViewModel)
			where T : BaseEmailServiceModel;
	}
}
