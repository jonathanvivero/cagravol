using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Entities.Common.Workflow
{
    public interface IWFNotifyPresetGroup
    {
        long Id { get; set; }
        string Name { get; set; }
        long? WFNotificationGroupId { get; set; }
        IWFGroup WFNotificationGroup { get; set; }        
    }
}
