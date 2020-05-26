using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class DateLaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string dependentProperty = null;

        public DateLaterThanAttribute(string dependentProperty)
        {
            this.dependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(this.dependentProperty))
            {
                var dependentValue = validationContext.ObjectType.GetProperty(this.dependentProperty).GetValue(validationContext.ObjectInstance, null);
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()) && dependentValue != null && !string.IsNullOrWhiteSpace(dependentValue.ToString()))
                {
                    DateTime parsedValue;
                    DateTime parsedDependentValue;
                    if (DateTime.TryParse(dependentValue.ToString(), out parsedDependentValue) && DateTime.TryParse(value.ToString(), out parsedValue) && parsedValue < parsedDependentValue)
                    {
                        return new ValidationResult(FormatErrorMessage(validationContext.MemberName));
                    }
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var displayName = metadata.GetDisplayName();
            var modelClientValidationRule = new ModelClientValidationRule()
            {
                ValidationType = "datelaterthan",
                ErrorMessage = string.Format(this.ErrorMessageString, displayName, "%0%")
            };

            modelClientValidationRule.ValidationParameters.Add("dependentproperty", this.dependentProperty);
            yield return modelClientValidationRule;
        }
    }
}
