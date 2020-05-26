using SGE.Cagravol.Domain.JSON.Customers;
using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.POCO.Files;

namespace SGE.Cagravol.Application.Services.Customers
{
    public interface ICustomerService
    {
        Task<IResultModel> DeleteFileAsync(CustomerFileDeleteRequest request);
        Task<IResultModel> DeleteGeneralSpaceFileAsync(CustomerFileDeleteRequest request);
        IResultModel DeleteFile(CustomerFileDeleteRequest request);
        IEnumerable<FilePOCO> ExtractCustomerFileList(ICollection<File> fileCollection);        
    }
}
