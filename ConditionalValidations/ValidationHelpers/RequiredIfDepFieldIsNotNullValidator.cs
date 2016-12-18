using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ConditionalValidations.ValidationHelpers
{
    public class RequiredIfDepFieldIsNotNullValidator : ValidationAttribute
    {
        private readonly string _dependentProperty;
        public RequiredIfDepFieldIsNotNullValidator(string dependentProperty)
        {
            _dependentProperty = dependentProperty;
        }

        protected override ValidationResult IsValid(object targetValue, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon 
            var field = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (field == null)
            {
                // field not valid - return an error
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, 
                    "Unknown property {0}", new[] { _dependentProperty }));
            }
            // get the value of the dependent property
            var otherPropertyValue = field.GetValue(validationContext.ObjectInstance, null);

            // trim spaces and convert dependent value to uppercase to support case senstive comparison
            if (otherPropertyValue != null && otherPropertyValue is string)
            {            
                otherPropertyValue = (otherPropertyValue as string).Length == 0 ? null : (otherPropertyValue as string).ToUpper();
            }

            // trim spaces and convert TargetValue to uppercase to support case senstive comparison
            if (targetValue != null && targetValue is string)
            {
                targetValue = (targetValue as string).Length == 0 ? null : (targetValue as string).ToUpper();
            }

            // compare the value against the target value
            if (otherPropertyValue != null && targetValue == null)
            {
                // validation failed - return an error
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture,
                    FormatErrorMessage(validationContext.DisplayName), new[] { _dependentProperty }));
            }
            // validation success - return success
            return ValidationResult.Success;
        }
    }
}