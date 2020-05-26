using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Base
{
	public interface IBaseUserBasedRepository<T>
		 where T : class,  IEntityUserBasedIdentity, new()
	{
        IResultModel Add(T entity);

        IResultServiceModel<int> Count();

        Task<IResultServiceModel<int>> CountAsync();

        IResultServiceModel<T> Create();

		IResultModel Delete(T entity);

        IResultServiceModel<T> Find(string id);

        Task<IResultServiceModel<T>> FindAsync(string id);

        IResultServiceModel<IEnumerable<T>> GetAll();

        IResultModel Remove(T entity);

        IResultModel Update(T entity);
        IResultModel SetForRemoval(T entity);

        IResultModel Save();

		Task<IResultModel> SaveAsync();
	}
}
