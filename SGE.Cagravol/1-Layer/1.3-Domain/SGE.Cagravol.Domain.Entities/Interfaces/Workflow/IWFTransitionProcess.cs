using SGE.Cagravol.Application.Core.Enums.Workflows;
using SGE.Cagravol.Application.ServiceModel;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFTransitionProcess
    {
        string Id { get; set; }
        long WFTransitionId { get; set; }
        long WFProcessId { get; set; }
        WFTransition WFTransition { get; set; }
        WFProcess WFProcess { get; set; }
    }
}
