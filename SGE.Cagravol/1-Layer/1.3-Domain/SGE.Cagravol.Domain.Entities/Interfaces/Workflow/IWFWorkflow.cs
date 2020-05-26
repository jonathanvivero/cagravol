using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFWorkflow
    {
        long Id { get; set; }
        long WFWorkflowVersion { get; set; }
        string Name { get; set; }
        string Notes { get; set; }
        ICollection<WFWorkflowTransition> WFTransitions { get; set; }
        ICollection<WFWorkflowState> WFStates { get; set; }
        ICollection<WFWorkflowRelatedEntity> WFWorkflowRelatedEntities { get; set; }
        
    }
}
