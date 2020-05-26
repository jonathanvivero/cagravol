using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public class WFEntityTransition<T>        
        : IWFEntityTransition<T>        
    {
        public long Id { get; set; }
        //public long WFWorkflowId { get; set; }
        //public long WFWorkflowVersion { get; set; }
        public long? WFNotificationGroupId { get; set; }
        public long? WFNotificationPresetGroupId { get; set; }
        public long WFDefaultStateOriginId { get; set; }
        public long WFDefaultStateDestinationId { get; set; }
        public bool CouldComment { get; set; }
        public bool MustComment { get; set; }
        public string Code { get; set; }

        //public virtual WFWorkflow WFWorkflow { get; set; }
        public virtual WFState WFDefaultStateOrigin { get; set; }
        public virtual WFState WFDefaultStateDestination { get; set; }
        public virtual WFNotifyPresetGroup WFNotificationPresetGroup { get; set; }
        public virtual WFGroup WFNotificationGroup { get; set; }
        public virtual ICollection<WFTransitionProcess> WFProcesses { get; set; }
        public virtual ICollection<WFTransitionRequirement> WFRequirements { get; set; }
        public virtual ICollection<IWFEntityProcess<T>> EntityWFProcesses { get; set; }
        public virtual ICollection<IWFEntityRequirement<T>> EntityWFRequirements { get; set; }
        public virtual ICollection<WFWorkflowTransition> WFWorkflows { get; set; }

        public WFEntityTransition()
        {            
            this.EntityWFProcesses = new HashSet<IWFEntityProcess<T>>();
            this.EntityWFRequirements = new HashSet<IWFEntityRequirement<T>>();

            this.WFProcesses = new HashSet<WFTransitionProcess>();
            this.WFRequirements = new HashSet<WFTransitionRequirement>();
            this.WFWorkflows = new HashSet<WFWorkflowTransition>();
        }

    }
}
