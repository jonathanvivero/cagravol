using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFEntityState<T>
    {
        long Id { get; set; }        
        long EntityId { get; set; }        
        long WFStateId { get; set; }
        string UserId { get; set; }
        DateTime TS { get; set; }
        
        WFState WFState { get; set; }
        User User {get;set;}
        T Entity { get; set; }
        //ICollection<WFEntityStateNote<T>> Notes { get; set; }
                
    }
}
