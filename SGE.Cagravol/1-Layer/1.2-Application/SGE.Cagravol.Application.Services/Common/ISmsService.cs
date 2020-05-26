using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
	public interface ISmsService
	{
		Task SendAsync(IdentityMessage message);
	}
}
