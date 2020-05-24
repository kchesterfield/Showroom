using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Showroom.Enrollment.Models
{
    public class EnrollmentResource : IValidatableObject
    {
        public string InsuranceCompany { get; set; }
        public List<Enrollee> Enrollees { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(InsuranceCompany))
            {
                IEnumerable<string> memberNames = new[] { "InsuranceComany" };
                yield return new ValidationResult(@"Insurance Comany can not be empty.", memberNames);
            }
        }
    }

    public class Enrollee
    {
        public string UserId { get; set; }
        public string FirstAndLastName { get; set; }
        public int Version { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(UserId))
            {
                IEnumerable<string> memberNames = new[] { "UserId" };
                yield return new ValidationResult(@"User Id can not be empty.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(FirstAndLastName))
            {
                IEnumerable<string> memberNames = new[] { "FirstAndLastName" };
                yield return new ValidationResult(@"First and last name can not be empty.", memberNames);
            }
            if (Version < 0)
            {
                IEnumerable<string> memberNames = new[] { "Version" };
                yield return new ValidationResult(@"Version must be a valid integer.", memberNames);
            }
        }
    }
}
