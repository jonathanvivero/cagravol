using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFState
    {
        long Id { get; set; }
        long WFWorkflowId { get; set; }
        long WFWorkflowVersion { get; set; }
        long? WFSecurityPresetGroupId { get; set; }
        long? WFGrantedGroupId { get; set; }
        long? WFDeniedGroupId { get; set; }
        string Code { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        
        IWFSecurityPresetGroup WFSecurityPresetGroup { get; set; }
        IWFGroup WFGrantedGroup { get; set; }
        IWFGroup WFDeniedGroup { get; set; }

        ICollection<IWFTransition> TransitionsFrom { get; set; }
        ICollection<IWFTransition> TransitionsTo { get; set; }

    }
}
