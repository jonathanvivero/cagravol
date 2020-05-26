using SGE.Cagravol.Domain.Entities.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFEntityStateNote<T> 
    {
        long Id { get; set; }
        long WFEntityStateId { get; set; }
        string UserId { get; set; }
        DateTime TS { get;set; }
        string Comment { get;set;}

        IWFEntityState<T> WFEntityState { get; set; }
        ISGEUser User { get; set; }
    }
}
