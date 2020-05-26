using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFEntityWorkflow<T>
    {
        long? WFWorkflowId { get; set; }
        long? WFWorkflowVersion { get; set; }
        WFWorkflow WFWorkflow { get; set; }
        //ICollection<WFEntityState<T>> History { get; set; }                
        
    }
}
