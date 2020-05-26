using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFRole
    {
        long Id { get; set; }        
        string RoleId { get; set; }
        long WFGroupId { get; set; }
        WFGroup WFGroup { get; set; }

        Role Role { get; set; }
    }
}
