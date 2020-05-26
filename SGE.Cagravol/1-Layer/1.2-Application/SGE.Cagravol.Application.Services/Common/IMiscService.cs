using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Common
{
    public interface IMiscService
    {
        IResultModel SetValue(string key, string value, DateTime limit);
        IResultModel SetValue(string key, string value);
        IResultServiceModel<IEnumerable<Misc>> GetByKey(string key);
        IResultServiceModel<string> GetValue(string key);
        IResultModel DeleteOutOfLimit();
        IResultServiceModel<string> GenerateDownloadExcelFileId(long projectId);
        IResultServiceModel<long> CheckDownloadExcelFileId(string guid);
        IResultModel RemoveExcelGUID(string guid);
    }
}
