using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Repositories.Customers
{
    public interface IBillDataTypeRepository
        : IBaseRepository<BillDataType>
    {        
    }
}
