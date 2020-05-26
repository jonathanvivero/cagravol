using SGE.Cagravol.Domain.POCO.Customers;
using SGE.Cagravol.Domain.POCO.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Projects
{
    public class ProjectGSpaceActivityResponse
    {
        public IEnumerable<FilePOCO> list { get; set; }        
    }
}
