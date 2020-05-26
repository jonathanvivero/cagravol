using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Base
{
    public interface IBaseRepository<T>
         where T : class,  IEntityIdentity, new()
    {
        IResultModel Add(T entity);

        IResultServiceModel<int> Count();

        Task<IResultServiceModel<int>> CountAsync();

        IResultServiceModel<T> Create();

        IResultModel Delete(T entity);

        IResultServiceModel<T> Find(long id);

        Task<IResultServiceModel<T>> FindAsync(long id);

        IResultServiceModel<IEnumerable<T>> GetAll();

        IResultModel Remove(T entity);

        IResultModel Update(T entity);
        IResultModel SetForRemoval(T entity);

        IResultModel Save();

        Task<IResultModel> SaveAsync();
    }
}
