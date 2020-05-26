using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFWorkflow
    {
        long Id { get; set; }
        long WFWorkflowVersion { get; set; }
        string Name { get; set; }
        string Notes { get; set; }
        ICollection<IWFTransition> WFTransitions { get; set; }
        ICollection<IWFState> WFStates { get; set; }
        ICollection<IWFWorkflowRelatedEntity> WFWorkflowRelatedEntities { get; set; }
        
    }
}
