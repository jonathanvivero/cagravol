using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFState 
        : IEntityIdentity, IWFState
    {
        public long Id { get; set; }
        //public long WFWorkflowId { get; set; }
        //public long WFWorkflowVersion { get; set; }
        public long? WFSecurityPresetGroupId { get; set; }
        public long? WFGrantedGroupId { get; set; }
        public long? WFDeniedGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }      
        //public virtual WFWorkflow WFWorkflow { get; set; }
        public virtual WFSecurityPresetGroup WFSecurityPresetGroup { get; set; }
        public virtual WFGroup WFGrantedGroup { get; set; }
        public virtual WFGroup WFDeniedGroup { get; set; }

        public virtual ICollection<WFWorkflowState> WFWorkflows { get; set; }
        public virtual ICollection<WFTransition> WFDefaultTransitionsFrom { get; set; }
        public virtual ICollection<WFTransition> WFDefaultTransitionsTo { get; set; }

        public WFState()
        {
            this.WFDefaultTransitionsFrom = new HashSet<WFTransition>();
            this.WFDefaultTransitionsTo = new HashSet<WFTransition>();
            this.WFWorkflows = new HashSet<WFWorkflowState>();
        }

    }
}
