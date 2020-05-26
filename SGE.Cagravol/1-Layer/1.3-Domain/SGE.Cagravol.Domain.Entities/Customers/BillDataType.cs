using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Customers
{
    public class BillDataType 
        : IEntityIdentity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string I18NCode { get; set; }
        public string Notes { get; set; }

    }
}
