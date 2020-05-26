using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Enums.Common.Common
{
    public enum EnumAppConfigurationFieldType : int
    {
        Unknown = 0,
        Text = 1,
        Numeric  = 2,
        NumericDecimal = 4,
        List = 8,
        Coords = 16        
    }
}
