using SGE.Cagravol.Application.Core.Enums;
using SGE.Cagravol.Application.Core.Enums.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.Entities.Common
{
    public class Log
        : IEntityIdentity
    {
        public long Id { get; set; }
        public string Notes { get; set; }        
    }
}
