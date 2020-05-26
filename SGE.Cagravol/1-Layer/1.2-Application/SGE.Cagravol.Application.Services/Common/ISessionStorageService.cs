using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
	public interface ISessionStorageService
	{
		T Get<T>(string name);

		void Set(string name, object value);
	}
}
