using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFSecurityPresetGroup
        : IEntityIdentity, IWFSecurityPresetGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? WFGrantedGroupId { get; set; }
        public long? WFDeniedGroupId { get; set; }

        public virtual WFGroup WFGrantedGroup { get; set; }
        public virtual WFGroup WFDeniedGroup { get; set; }
    }
}
