using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Customers
{
    public class CustomerFileUpdateRequest
    {
        public long id { get; set; }
        public string name { get; set; }
        public string fileNotes { get; set; }
        public long fileTypeId { get; set; }        
        public string userName { get; set; }
    }
}
