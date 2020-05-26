using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class NumberFormatAttribute : RegularExpressionAttribute
    {
        public NumberFormatAttribute()
            : base(@"^-?(?:\d*|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$")
        { }
    }
}
