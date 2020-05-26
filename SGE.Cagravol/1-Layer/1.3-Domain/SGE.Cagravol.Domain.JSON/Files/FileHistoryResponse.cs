using SGE.Cagravol.Domain.POCO.Files;
using SGE.Cagravol.Domain.POCO.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Files
{
    public class FileHistoryResponse
    {
        public FilePOCO File { get; set; }
        public IEnumerable<WFFileStatePOCO> States { get; set; }

    }
}
