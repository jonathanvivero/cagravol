using SGE.Cagravol.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Infrastructure.Data.Migrations.Updates
{
    public interface IPortalUpdate
    {
        AppConfiguration keyVersion {get;set;}
        AppConfiguration DoUpdate();

    }
}
