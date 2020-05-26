using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Files;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGE.Cagravol.Domain.Entities.Workflows;
using SGE.Cagravol.Domain.Entities.Identity;

namespace SGE.Cagravol.Domain.Entities.History
{

    public class WFFileState
        : WFEntityState<File>
    {
        public virtual ICollection<WFFileStateNote> Notes { get; set;}

        public WFFileState()
        {
            this.Notes = new HashSet<WFFileStateNote>();            
        }
    }
}
