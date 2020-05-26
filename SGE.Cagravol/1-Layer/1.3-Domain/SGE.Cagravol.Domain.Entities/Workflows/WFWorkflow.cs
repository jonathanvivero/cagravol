using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFWorkflow
        : IEntityIdentity, IWFWorkflow
    {
        public long Id { get; set; }
        public long WFWorkflowVersion { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<WFWorkflowTransition> WFTransitions { get; set; }
        public virtual ICollection<WFWorkflowState> WFStates { get; set; }
        public virtual ICollection<WFWorkflowRelatedEntity> WFWorkflowRelatedEntities { get; set; }

        public WFWorkflow()
        {
            this.WFTransitions = new HashSet<WFWorkflowTransition>();
            this.WFStates = new HashSet<WFWorkflowState>();
            this.WFWorkflowRelatedEntities = new HashSet<WFWorkflowRelatedEntity>();
        }


    }
}
