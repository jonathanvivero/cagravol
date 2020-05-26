using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class PositiveIntegerFormatAttribute : RegularExpressionAttribute
    {
        public PositiveIntegerFormatAttribute()
            : base(@"^[1-9]\d*$")
        { }
    }
}
