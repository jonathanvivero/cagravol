using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGE.Cagravol.Application.Core.DataAnnotations
{
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private readonly string dependentProperty = null;
        private readonly string dependentValue;

        public RequiredIfAttribute(string dependentProperty, string dependentValue)
        {
            this.dependentProperty = dependentProperty;
            this.dependentValue = dependentValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(this.dependentProperty))
            {
                var isRequired = validationContext.ObjectType.GetProperty(this.dependentProperty).GetValue(validationContext.ObjectInstance, null);
                if (isRequired != null && isRequired.ToString().Equals(this.dependentValue))
                {
                    if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        var fieldName = validationContext.MemberName;
                        if (string.IsNullOrEmpty(fieldName))
                        {
                            var fieldNameProperty = validationContext.ObjectInstance.GetType().GetProperty("FieldName");
                            if (fieldNameProperty != null)
                            {
                                fieldName = (string)fieldNameProperty.GetValue(validationContext.ObjectInstance, null);
                            }
                        }
                        var errorMessage = FormatErrorMessage(fieldName);
                        return new ValidationResult(errorMessage);
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
                ValidationType = "requiredif",
                ErrorMessage = string.Format(this.ErrorMessageString, displayName)
            };

            modelClientValidationRule.ValidationParameters.Add("dependentproperty", this.dependentProperty);
            modelClientValidationRule.ValidationParameters.Add("dependentvalue", this.dependentValue);
            yield return modelClientValidationRule;
        }
    }
}
