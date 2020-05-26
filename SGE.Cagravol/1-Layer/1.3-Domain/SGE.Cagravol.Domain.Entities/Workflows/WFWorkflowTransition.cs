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
    public class WFWorkflowTransition
        : IEntityIdentity, IWFWorkflowTransition
    {
        public long Id { get; set; }
        public long WFTransitionId { get; set; }
        public long WFWorkflowId { get; set; }
        public long WFWorkflowVersion { get; set; }
        public long WFWorkflowStateOriginId { get; set; }
        public long WFWorkflowStateDestinationId { get; set; }

        public virtual WFTransition WFTransition { get; set; }
        public virtual WFWorkflow WFWorkflow { get; set; }
        public virtual WFWorkflowState WFWorkflowStateOrigin { get; set; }
        public virtual WFWorkflowState WFWorkflowStateDestination { get; set; }        
    }
}
