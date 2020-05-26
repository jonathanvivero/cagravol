using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFSecurityPresetGroup
    {
        long Id { get; set; }
        string Name { get; set; }
        long? WFGrantedGroupId { get; set; }
        long? WFDeniedGroupId { get; set; }
        IWFGroup WFGrantedGroup { get; set; }        
        IWFGroup WFDeniedGroup { get; set; }
    }
}
