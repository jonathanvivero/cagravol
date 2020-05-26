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
    public class WFWorkflowRelatedEntity
        : IEntityIdentity, IWFWorkflowRelatedEntity
    {
        public long Id { get; set; }
        public long WFWorkflowId { get; set; }
        public string Name { get; set; }
        public WFEntityTypeEnum EntityType { get; set; }

        public virtual WFWorkflow WFWorkflow { get; set; }

    }
}
