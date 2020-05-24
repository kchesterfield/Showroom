using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Showroom.Enrollment.Models
{
    public class EnrollmentRequest : IValidatableObject
    {
        public string CsvString { get; set; }

        public string CsvBase64 { get; set; }

        public string ToCsvString()
        {
            return !string.IsNullOrEmpty(CsvString) ? CsvString : Encoding.UTF8.GetString(Convert.FromBase64String(CsvBase64));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrWhiteSpace(CsvString) && string.IsNullOrWhiteSpace(CsvBase64))
            {
                IEnumerable<string> memberNames = new[] { "CsvString", "CsvBase64" };
                yield return new ValidationResult(@"CsvString or CsvBase64 must be supplied in the call.", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(CsvString) && !string.IsNullOrWhiteSpace(CsvBase64))
            {
                IEnumerable<string> memberNames = new[] { "CsvString", "CsvBase64" };
                yield return new ValidationResult(@"Only one of the values can be suppled: CsvString | CsvBase64", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(CsvBase64) && !Regex.IsMatch(CsvBase64, @"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)?$"))
            {
                IEnumerable<string> memberNames = new[] { "CsvString", "CsvBase64" };
                yield return new ValidationResult(@"CsvBase64 is not properly Base64 encoded.", memberNames);
            }
        }
    }
}
