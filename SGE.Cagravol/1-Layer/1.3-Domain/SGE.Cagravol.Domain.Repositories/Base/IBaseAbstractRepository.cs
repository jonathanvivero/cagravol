using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Base
{
    public interface IBaseAbstractRepository<T>
       where T : class,IEntityIdentity
    {
        IResultModel Add(T entity);

        IResultServiceModel<int> Count();

        IResultModel Delete(T entity);

        IResultServiceModel<T> Find(long id);

        IResultServiceModel<IEnumerable<T>> GetAll();

        IResultModel Remove(T entity);

        IResultModel Update(T entity);
        IResultModel SetForRemoval(T entity);
        IResultModel Save();
    }
}
