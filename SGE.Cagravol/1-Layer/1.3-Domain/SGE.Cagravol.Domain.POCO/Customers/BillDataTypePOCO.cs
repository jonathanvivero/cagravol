using SGE.Cagravol.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.Customers
{
    public class BillDataTypePOCO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string I18NCode { get; set; }
        public string Notes { get; set; }

        public BillDataTypePOCO() { }
        public BillDataTypePOCO(BillDataType entity) 
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.I18NCode = entity.I18NCode;
            this.Notes = entity.Notes;    
        }
    }
}
