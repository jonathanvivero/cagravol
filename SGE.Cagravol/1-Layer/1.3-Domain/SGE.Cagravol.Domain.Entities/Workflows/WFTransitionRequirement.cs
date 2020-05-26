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
    public class WFTransitionRequirement
        :IEntityNoAutoIdentity, IWFTransitionRequirement
    {
        public string Id { get; set; }
        public long WFTransitionId { get; set; }
        public long WFRequirementId { get; set; }
        public virtual WFTransition WFTransition { get; set; }
        public virtual WFRequirement WFRequirement { get; set; }
    }
}
