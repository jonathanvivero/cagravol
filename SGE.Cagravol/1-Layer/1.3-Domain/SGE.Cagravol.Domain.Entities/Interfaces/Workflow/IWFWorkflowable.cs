using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFWorkflowable
    {
        long Id { get; set; }
        long? WFWorkflowId { get; set; }
        long? WFWorkflowVersion { get; set; }        
        WFWorkflow WFWorkflow { get; set; }        
    }
}
