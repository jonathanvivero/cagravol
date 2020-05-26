﻿using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Identity;
using SGE.Cagravol.Domain.Entities.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Workflows
{
    public class WFRole
        :IEntityIdentity, IWFRole
    {
        public long Id { get; set; }
        public string RoleId { get; set; }
        public long WFGroupId { get; set; }

        public virtual WFGroup WFGroup { get; set; }
        public virtual Role Role { get; set; }
    }
}
