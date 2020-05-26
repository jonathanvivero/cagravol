using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.POCO.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Customers
{
    public class CustomerActivityResponse
    {
        public IEnumerable<FilePOCO> list { get; set; }
        public long projectId { get; set; }
        public bool hasRecharge { get; set; }
        public bool willHaveRecharge { get; set; }
        public bool isOutOfDate { get; set; }
        public string alertMessage { get; set; }
    }
}
