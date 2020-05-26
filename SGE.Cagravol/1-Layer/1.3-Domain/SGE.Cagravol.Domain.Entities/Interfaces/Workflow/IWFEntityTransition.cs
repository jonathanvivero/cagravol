using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFEntityTransition<T> 
        : IWFTransition
    {        
        ICollection<IWFEntityProcess<T>> EntityWFProcesses { get; set; }
        ICollection<IWFEntityRequirement<T>> EntityWFRequirements { get; set; }
    }    
}
