using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFNotifyPresetGroup
        :IEntityIdentity, IWFNotifyPresetGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? WFNotificationGroupId { get; set; }

        public virtual WFGroup WFNotificationGroup { get; set; }        
        
    }
}
