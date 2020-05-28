using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Showroom.User.Models
{
    public class UserSearch : UserResource, IValidatableObject
    {
        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
            {
                IEnumerable<string> memberNames = new[] { "LastName" };
                yield return new ValidationResult(@"Last Name can not be empty if First Name was provided.", memberNames);
            }
        }
    }
}
