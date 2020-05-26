using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFTransition
    {
        long Id { get; set; }
        //long WFWorkflowId { get; set; }
        //long WFWorkflowVersion { get; set; }
        long? WFNotificationGroupId { get; set; }
        long? WFNotificationPresetGroupId { get; set; }
        long WFDefaultStateOriginId { get; set; }
        long WFDefaultStateDestinationId { get; set; }
        bool CouldComment { get; set; }
        bool MustComment { get; set; }
        string Code { get; set; }
        
        //WFWorkflow WFWorkflow { get; set; }
        WFState WFDefaultStateOrigin { get; set; }
        WFState WFDefaultStateDestination { get; set; }
        WFNotifyPresetGroup WFNotificationPresetGroup { get; set; }
        WFGroup WFNotificationGroup { get; set; }
        ICollection<WFWorkflowTransition> WFWorkflows { get; set; }
        ICollection<WFTransitionProcess> WFProcesses { get; set; }
        ICollection<WFTransitionRequirement> WFRequirements { get; set; }
    }
}
