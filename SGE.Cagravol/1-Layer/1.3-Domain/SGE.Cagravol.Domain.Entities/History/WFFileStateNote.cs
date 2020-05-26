using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.History
{

    public class WFFileStateNote
        : WFEntityStateNote<File>
    {        
        public virtual WFFileState WFEntityState { get ; set ; }
    }   
}
