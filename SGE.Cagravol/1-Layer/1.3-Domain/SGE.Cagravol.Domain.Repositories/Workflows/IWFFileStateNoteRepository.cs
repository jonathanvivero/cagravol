using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Files;

namespace SGE.Cagravol.Domain.Repositories.History
{
    public interface IWFFileStateNoteRepository
        : IBaseRepository<WFFileStateNote>
    {
        IResultServiceModel<IEnumerable<WFFileStateNote>> GetListByFileId(long id);
        IResultServiceModel<IEnumerable<WFFileStateNote>> GetListByFileHistoryItemId(long id);
    }
}
