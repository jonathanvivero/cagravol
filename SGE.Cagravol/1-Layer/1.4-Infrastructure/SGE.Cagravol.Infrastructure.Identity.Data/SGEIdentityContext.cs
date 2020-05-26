using Microsoft.AspNet.Identity.EntityFramework;
using SGE.Cagravol.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Identity.Data
{
    public class SGEIdentityContext : IdentityDbContext<User>, ISGEIdentityContext
    {
        public SGEIdentityContext()
            : base("SGE.Cagravol.Infrastructure.Identity.Data.SGEIdentityContext", throwIfV1Schema: false)
        {
        }

        public static SGEIdentityContext Create()
        {
            return new SGEIdentityContext();
        }
    }
}
