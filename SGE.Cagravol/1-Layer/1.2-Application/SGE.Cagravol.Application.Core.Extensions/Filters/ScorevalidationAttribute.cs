using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Application.Core.Extensions.Filters
{
    public class ScorevalidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            return base.IsValid(value, validationContext);
        }
    }
}
