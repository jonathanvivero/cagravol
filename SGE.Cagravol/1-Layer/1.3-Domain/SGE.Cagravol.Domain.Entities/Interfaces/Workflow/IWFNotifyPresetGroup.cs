using SGE.Cagravol.Domain.Entities.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Interfaces.Workflow
{
    public interface IWFNotifyPresetGroup
    {
        long Id { get; set; }
        string Name { get; set; }
        long? WFNotificationGroupId { get; set; }
        WFGroup WFNotificationGroup { get; set; }        
    }
}
