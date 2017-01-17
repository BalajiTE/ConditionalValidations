using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ConditionalValidations.ValidationHelpers
{
    public class RequiredIfNotRegExValidator : ValidationAttribute
    {
        private Regex _regex = null;
        private RequiredAttribute _innerAttribute = new RequiredAttribute();
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }
        public RequiredIfNotRegExValidator(string regExTypeProperty, string dependentProperty, object targetValue)
        {
            _regex = new Regex(regExTypeProperty);
            this.DependentProperty = dependentProperty;
            this.TargetValue = targetValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);

            if (field != null)
            {
                var phoneNumber = Convert.ToString(value, CultureInfo.CurrentCulture);

                // get the value of the dependent property
                object dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                // trim spaces and convert dependent value to uppercase to support case senstive comparison
                if (dependentValue != null && dependentValue is string)
                {
                    dependentValue = (dependentValue as string).Trim();
                    dependentValue = (dependentValue as string).Length == 0 ? null : (dependentValue as string).ToUpper();
                }

                // trim spaces and convert TargetValue to uppercase to support case senstive comparison
                if (TargetValue != null && TargetValue is string)
                {
                    TargetValue = (TargetValue as string).Trim();
                    TargetValue = (TargetValue as string).Length == 0 ? null : (TargetValue as string).ToUpper();
                }

                // compare the value against the target value
                if ((dependentValue == null && TargetValue.Equals("") ||
                    (dependentValue == null && !TargetValue.Equals("") ||
                    (dependentValue != null && !dependentValue.Equals(this.TargetValue)))))
                {
                    // match => means we should try validating this field
                    if (_innerAttribute.IsValid(value))
                    {
                        var match = _regex.Match(phoneNumber);
                        if (!match.Success)
                            // validation failed - return an error
                            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName),
                                                        new[] { validationContext.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}