using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFGroup
    {
        long Id { get; set; }
        string Name { get; set; }
        bool IsPreset { get; set; }
        ICollection<WFRole> Roles { get; set; }
        ICollection<WFUser> Users { get; set; }        
    }
}
