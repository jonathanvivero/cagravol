using SGE.Cagravol.Entities.Common.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public class WFEntityTransition<T>        
        : IWFEntityTransition<T>        
    {
        public long Id { get; set; }
        public long WFWorkflowId { get; set; }
        public long WFWorkflowVersion { get; set; }
        public long? WFNotificationGroupId { get; set; }
        public long? WFNotificationPresetGroupId { get; set; }
        public long WFStateOriginId { get; set; }
        public long WFStateDestinationId { get; set; }
        public bool CouldComment { get; set; }
        public bool MustComment { get; set; }
        public string Code { get; set; }
        public IWFWorkflow WFWorkflow { get; set; }
        public IWFState WFStateOrigin { get; set; }
        public IWFState WFStateDestination { get; set; }
        public IWFNotifyPresetGroup WFNotificationPresetGroup { get; set; }
        public IWFGroup WFNotificationGroup { get; set; }        
        public ICollection<IWFProcess> Processes { get; set; }
        public ICollection<IWFRequirement> Requirements { get; set; }
        public ICollection<IWFEntityProcess<T>> EntityProcesses { get; set; }
        public ICollection<IWFEntityRequirement<T>> EntityRequirements { get; set; }

        public WFEntityTransition()
        {            
            this.EntityProcesses = new HashSet<IWFEntityProcess<T>>();
        }

    }
}
