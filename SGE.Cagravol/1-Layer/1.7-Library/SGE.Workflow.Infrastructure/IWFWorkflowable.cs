using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFWorkflowable
    {
        long Id { get; set; }
        long? WFWorkflowId { get; set; }
        long? WFWorkflowVersion { get; set; }        
        IWFWorkflow WFWorkflow { get; set; }        
    }
}
