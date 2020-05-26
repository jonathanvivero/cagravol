using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Application.Core.Enums.Common;

namespace SGE.Cagravol.Domain.Repositories.Common
{
    public interface IMiscRepository
        : IBaseRepository<Misc>
    {
        IResultModel SetValue(string key, string value, DateTime limit);
        IResultModel SetValue(string key, string value);
        IResultServiceModel<IEnumerable<Misc>> GetByKey(string key);
        IResultServiceModel<Misc> GetByKeyWithValue(string key, string value, bool exactMatch = false);
        IResultServiceModel<string> GetValue(string key);
        IResultModel DeleteOutOfLimit();

    }
}
