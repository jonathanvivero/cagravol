using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.JSON.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.History
{
    public interface IHistoryService
    {
        IResultServiceModel<FileHistoryResponse> GetFileHistoryByFileAndUser(long fileId, User user, bool isOnlyCustomer);
        IResultServiceModel<FileStateCommentResponse> AddCommentToFileState(FileStateCommentRequest request, User user);
    }
}
