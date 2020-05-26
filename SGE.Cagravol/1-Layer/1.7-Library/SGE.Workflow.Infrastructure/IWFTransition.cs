using SGE.Cagravol.Application.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFTransition
    {
        long Id { get; set; }
        long WFWorkflowId { get; set; }
        long WFWorkflowVersion { get; set; }
        long? WFNotificationGroupId { get; set; }
        long? WFNotificationPresetGroupId { get; set; }
        long WFStateOriginId { get; set; }
        long WFStateDestinationId { get; set; }
        bool CouldComment { get; set; }
        bool MustComment { get; set; }
        string Code { get; set; }
        
        IWFWorkflow WFWorkflow { get; set; }
        IWFState WFStateOrigin { get; set; }
        IWFState WFStateDestination { get; set; }
        IWFNotifyPresetGroup WFNotificationPresetGroup { get; set; }
        IWFGroup WFNotificationGroup { get; set; }        
        
        ICollection<IWFProcess> Processes { get; set; }
        ICollection<IWFRequirement> Requirements { get; set; }
    }
}
