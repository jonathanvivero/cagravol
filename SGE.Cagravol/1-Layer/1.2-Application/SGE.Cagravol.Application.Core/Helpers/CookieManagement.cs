using SGE.Cagravol.Application.Core.Enums.Identity;
using System;
using System.Web;


namespace SGE.Cagravol.Application.Core.Helpers
{
	public static class CookieManagement
	{
		public const string UserTypeCookieName = "userType";

		public static void CreateCookie()
		{
			var response = HttpContext.Current.Response;
			var cookie = new HttpCookie(UserTypeCookieName)
			{
				Expires = DateTime.Now.AddDays(5),
				Value = UserTypesEnum.Customer.ToString()
			};

			response.Cookies.Add(cookie);
		}

		public static UserTypesEnum GetUserType()
		{
			var request = HttpContext.Current.Request;
			UserTypesEnum result;

			if (request.Cookies[UserTypeCookieName] != null)
			{
				result = (UserTypesEnum)Enum.Parse(typeof(UserTypesEnum), request.Cookies[UserTypeCookieName].Value);
			}
			else
			{
				result = UserTypesEnum.Customer;
			}

			return result;
		}
	}
}
