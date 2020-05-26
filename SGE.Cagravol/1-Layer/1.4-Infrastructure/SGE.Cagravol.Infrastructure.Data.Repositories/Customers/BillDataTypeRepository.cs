using SGE.Cagravol.Domain.Entities.Customers;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Repositories.Base;
using SGE.Cagravol.Domain.Repositories.Customers;
using SGE.Cagravol.Infrastructure.Data;
using SGE.Cagravol.Infrastructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Insfractructure.Data.Repositories.Customers
{
    public class BillDataTypeRepository
        : BaseRepository<BillDataType>, IBillDataTypeRepository
    {
        public BillDataTypeRepository(ISGEContext context)
			: base(context)
		{
		}		

    }
}
