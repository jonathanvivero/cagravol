using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Base
{
    public interface IBaseNoAutoIdAbstractRepository<T>
      where T : class,IEntityNoAutoIdentity
    {
        IResultModel Add(T entity);

        IResultServiceModel<int> Count();

        IResultModel Delete(T entity);

        IResultServiceModel<T> Find(string id);

        IResultServiceModel<IEnumerable<T>> GetAll();

        IResultModel Remove(T entity);

        IResultModel Update(T entity);
        IResultModel SetForRemoval(T entity);
        IResultModel Save();
    }
}
