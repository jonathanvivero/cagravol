using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFSecurityPresetGroup
    {
        long Id { get; set; }
        string Name { get; set; }
        long? WFGrantedGroupId { get; set; }
        long? WFDeniedGroupId { get; set; }
        WFGroup WFGrantedGroup { get; set; }        
        WFGroup WFDeniedGroup { get; set; }
    }
}
