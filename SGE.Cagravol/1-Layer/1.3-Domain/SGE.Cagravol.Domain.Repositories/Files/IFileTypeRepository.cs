using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.JSON.Files;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Files
{
    public interface IFileTypeRepository
        : IBaseRepository<FileType>
    {
        IResultServiceModel<IEnumerable<FileTypeJSON>> GetAllJSON();
    }
}
