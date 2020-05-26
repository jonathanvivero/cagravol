using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Files
{
    public interface ICustomerFileService
    {
        IResultServiceModel<IEnumerable<File>> GetCustomerFilesByProject(string userName, long projectId);
        Task<IResultServiceModel<IEnumerable<File>>> GetCustomerFilesByProjectAsync(string userName, long projectId);
    }
}
