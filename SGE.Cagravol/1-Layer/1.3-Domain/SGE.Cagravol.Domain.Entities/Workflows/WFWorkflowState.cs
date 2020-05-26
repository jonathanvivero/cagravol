using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFWorkflowState
        : IEntityIdentity, IWFWorkflowState
    {
        public long Id { get; set; }
        public long WFStateId { get; set; }
        public long WFWorkflowId { get; set; }
        public long WFWorkflowVersion { get; set; }
        public bool IsInitial { get; set; }        
        public virtual WFState WFState { get; set; }
        public virtual WFWorkflow WFWorkflow { get; set; }

        public virtual ICollection<WFWorkflowTransition> WFWorkflowTransitionsFrom { get; set; }
        public virtual ICollection<WFWorkflowTransition> WFWorkflowTransitionsTo { get; set; }


    }
}
