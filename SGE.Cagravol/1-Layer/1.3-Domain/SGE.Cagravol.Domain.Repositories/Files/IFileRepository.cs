using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Files
{
    public interface IFileRepository
        : IBaseRepository<File>
    {
        IResultServiceModel<IEnumerable<File>> GetListByCustomerId(long id);
        IResultServiceModel<File> FindByIdAndUser(long fileId, string userId);
        IResultServiceModel<File> GetCompleteById(long id);
        IResultServiceModel<File> FindGSByIdAndProject(long id, long projectId);
        IResultModel RemoveAllByCustomer(long customerId);
        
    }
}
