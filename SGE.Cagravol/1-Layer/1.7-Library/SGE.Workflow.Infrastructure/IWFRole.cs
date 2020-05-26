using SGE.Cagravol.Domain.Entities.Common.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFRole
    {
        long Id { get; set; }        
        string RoleId { get; set; }
        long WFGroupId { get; set; }
        IWFGroup WFGroup { get; set; }

        ISGERole Role { get; set; }
    }
}
