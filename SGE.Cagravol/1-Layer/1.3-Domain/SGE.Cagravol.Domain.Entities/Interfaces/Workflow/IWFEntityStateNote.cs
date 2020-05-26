using SGE.Cagravol.Domain.Entities.History;
using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFEntityStateNote<T> 
    {
        long Id { get; set; }
        long WFEntityStateId { get; set; }
        string UserId { get; set; }
        DateTime TS { get;set; }
        string Comment { get;set;}
        //WFEntityState<T> WFEntityState { get; set; }
        User User { get; set; }
        
    }
}
