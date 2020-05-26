using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class NonUnicodeAttribute : RegularExpressionAttribute
    {
        public NonUnicodeAttribute()
            : base(@"[0-9a-zA-Z_-]+")
        { }
    }
}
