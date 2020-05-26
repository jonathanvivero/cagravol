using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class CustomPhoneAttribute : RegularExpressionAttribute
    {
        public CustomPhoneAttribute()
            : base(@"^((\+*[1\-]{2,2}[0-9]{1,3}[\s*]{0,1}|\+*[0-9]{0,3}[\s*]{0,1})|)[0-9]{0,15}$")
        { }
    }
}
