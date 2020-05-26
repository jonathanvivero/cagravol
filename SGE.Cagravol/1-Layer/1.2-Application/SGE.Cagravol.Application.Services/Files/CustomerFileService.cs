using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Services.Files
{
    public class CustomerFileService 
        : ICustomerFileService
    {
        public IResultServiceModel<IEnumerable<File>> GetCustomerFilesByProject(string userName, long projectId)
        {
            IResultServiceModel<IEnumerable<File>> rsm = new ResultServiceModel<IEnumerable<File>>();

            return rsm.OnSuccess(Enumerable.Empty<File>());
        }
        public Task<IResultServiceModel<IEnumerable<File>>> GetCustomerFilesByProjectAsync(string userName, long projectId)
        {
            IResultServiceModel<IEnumerable<File>> rsm = new ResultServiceModel<IEnumerable<File>>();

            return Task.FromResult(rsm.OnSuccess(Enumerable.Empty<File>()));
        }
    }
}
