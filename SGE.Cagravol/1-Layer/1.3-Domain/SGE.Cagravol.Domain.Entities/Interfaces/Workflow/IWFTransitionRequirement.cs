using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFTransitionRequirement
    {
        long WFTransitionId { get; set; }
        long WFRequirementId { get; set; }
        WFTransition WFTransition { get; set; }
        WFRequirement WFRequirement { get; set; }
    }
}
