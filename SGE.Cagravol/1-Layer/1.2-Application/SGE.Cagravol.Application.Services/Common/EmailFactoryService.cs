using SGE.Cagravol.Application.Core.Helpers;
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
	/// Email factory.
	/// </summary>
	public sealed class EmailFactoryService : IEmailFactoryService
	{
		private readonly IHtmlToViewHelper htmlToViewHelper;

		public EmailFactoryService(IHtmlToViewHelper htmlToViewHelper)
		{
			this.htmlToViewHelper = htmlToViewHelper;
		}

		/// <summary>
		/// Gets the email.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="controller">The controller.</param>
		/// <param name="emailViewModel">The email view model.</param>
		/// <returns></returns>
		public ResultEmailServiceModel GetEmail<T>(Controller controller, T emailViewModel)
			where T : BaseEmailServiceModel
		{
			var viewName = emailViewModel.GetType().Name;
			var resultEmailViewModel = new ResultEmailServiceModel();
			viewName = viewName.Replace("ViewModel", string.Empty);

			controller.ViewData.Model = emailViewModel;

			var result = this.htmlToViewHelper.GetHtmlFromRazorView(viewName, controller.ViewData.Model, controller);
			resultEmailViewModel.Body = result;
			resultEmailViewModel.Subject = emailViewModel.Subject;

			return resultEmailViewModel;
		}
	}
}
