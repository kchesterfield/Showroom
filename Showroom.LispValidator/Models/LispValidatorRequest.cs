using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Showroom.LispValidator.Models
{
    public class LispValidatorRequest : IValidatableObject
    {
        public string LispCodeAsBase64String { get; set; }
        public string LispCodeAsString { get; set; }
        public string RequestToString() 
        { 
            return !string.IsNullOrEmpty(LispCodeAsString) ? LispCodeAsString : Encoding.UTF8.GetString(Convert.FromBase64String(LispCodeAsBase64String));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(LispCodeAsBase64String) && string.IsNullOrEmpty(LispCodeAsString))
            {
                IEnumerable<string> memberNames = new[] { "LispCodeAsBase64String", "LispCodeAsString" };
                yield return new ValidationResult(@"One of the following values must have a valid string value: lispCodeAsBase64String | lispCodeAsString", memberNames);
            }
            if (!string.IsNullOrEmpty(LispCodeAsBase64String) && !string.IsNullOrEmpty(LispCodeAsString))
            {
                IEnumerable<string> memberNames = new[] { "LispCodeAsBase64String", "LispCodeAsString" };
                yield return new ValidationResult(@"Only supply one of the following values: lispCodeAsBase64String | lispCodeAsString", memberNames);
            }
            if (!string.IsNullOrEmpty(LispCodeAsBase64String) && !Regex.IsMatch(LispCodeAsBase64String, @"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$"))
            {
                IEnumerable<string> memberNames = new[] { "LispCodeAsBase64String" };
                yield return new ValidationResult(@"LispCodeAsBase64String must match on the following Regex: ^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$", memberNames);
            }
        }
    }
}
