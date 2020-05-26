using SGE.Cagravol.Domain.Entities.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFEntityState<T>
    {
        long Id { get; set; }
        long WFWorkflowId { get; set; }
        long WFWorkflowVersion { get; set; }
        long WFCurrentStateId { get; set; }
        long EntityId { get; set; }
        string UserId { get; set; }
        DateTime TS { get; set; }
        
        T Entity { get; set; }
        IWFWorkflow WFWorkflow { get; set; }
        IWFState WFState { get; set; }
        ISGEUser User {get;set;}

        ICollection<IWFEntityStateNote<T>> Notes { get; set; }
                
    }
}
