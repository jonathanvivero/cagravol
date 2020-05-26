using SGE.Cagravol.Domain.POCO.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.JSON.Workflows
{
    public class WorkflowMoveFileResponse
    {
        public WFFileStatePOCO wfCurrentState { get; set; }
    }
}
