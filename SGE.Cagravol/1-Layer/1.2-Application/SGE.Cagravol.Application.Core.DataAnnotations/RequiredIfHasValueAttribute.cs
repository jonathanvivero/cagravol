using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class RequiredIfHasValueAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string dependentProperty = null;

        public RequiredIfHasValueAttribute(string dependentProperty)
        {
            this.dependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(this.dependentProperty))
            {
                var dependentPropertyValue = validationContext.ObjectType.GetProperty(this.dependentProperty).GetValue(validationContext.ObjectInstance, null);
                if (dependentPropertyValue != null && !string.IsNullOrWhiteSpace(dependentProperty.ToString()))
                {
                    if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                        return ValidationResult.Success;
                    else
                        return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule() { ValidationType = "requieredifhasvalue", ErrorMessage = string.Format(this.ErrorMessageString, metadata.DisplayName) };
            modelClientValidationRule.ValidationParameters.Add("dependentproperty", this.dependentProperty);
            yield return modelClientValidationRule;
        }
    }
}
