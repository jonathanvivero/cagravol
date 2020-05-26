using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFWorkflowTransition
    {
        long Id { get; set; }
        long WFTransitionId { get; set; }
        long WFWorkflowId { get; set; }
        long WFWorkflowVersion { get; set; }
        long WFWorkflowStateOriginId { get; set; }
        long WFWorkflowStateDestinationId { get; set; }

        WFTransition WFTransition { get; set; }
        WFWorkflow WFWorkflow { get; set; }
        WFWorkflowState WFWorkflowStateOrigin { get; set; }
        WFWorkflowState WFWorkflowStateDestination { get; set; }        
    }
}
