using SGE.Cagravol.Domain.Entities.Common;
using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Projects
{
    public class UserProject 
        : IEntityIdentity
    {
        public long Id { get; set; }
        public long ProjectId { get;set; }
        public string UserId { get; set; }
        public bool IsAuthorized { get;set; }
        public bool IsOwner { get; set; }        
        
        public virtual Project Project { get; set; }        
        public virtual User User { get; set; }

    }
}
