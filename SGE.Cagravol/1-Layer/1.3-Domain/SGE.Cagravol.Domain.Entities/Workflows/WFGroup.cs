using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFGroup
        : IEntityIdentity, IWFGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsPreset { get; set; }
        public virtual ICollection<WFRole> Roles { get; set; }
        public virtual ICollection<WFUser> Users { get; set; }

        public WFGroup()
        {
            this.Roles = new HashSet<WFRole>();
            this.Users = new HashSet<WFUser>();
        }
    }
}
