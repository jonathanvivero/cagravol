using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFState
    {
        long Id { get; set; }
        //long WFWorkflowId { get; set; }
        //long WFWorkflowVersion { get; set; }
        long? WFSecurityPresetGroupId { get; set; }
        long? WFGrantedGroupId { get; set; }
        long? WFDeniedGroupId { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        //WFWorkflow WFWorkflow { get; set; }
        WFSecurityPresetGroup WFSecurityPresetGroup { get; set; }
        WFGroup WFGrantedGroup { get; set; }
        WFGroup WFDeniedGroup { get; set; }

        ICollection<WFWorkflowState> WFWorkflows { get; set; }
        //ICollection<WFTransition> TransitionsFrom { get; set; }
        //ICollection<WFTransition> TransitionsTo { get; set; }

    }
}
