using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Showroom.User.Models
{
    public class UserResource : IValidatableObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NpiNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                IEnumerable<string> memberNames = new[] { "FirstName" };
                yield return new ValidationResult(@"First name can not be empty.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                IEnumerable<string> memberNames = new[] { "LastName" };
                yield return new ValidationResult(@"Last name can not be empty.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(NpiNumber))
            {
                IEnumerable<string> memberNames = new[] { "NpiNumber" };
                yield return new ValidationResult(@"NPI number can not be empty.", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(NpiNumber) && !Regex.IsMatch(NpiNumber, @"^\d{10}$"))
            {
                IEnumerable<string> memberNames = new[] { "NpiNumber" };
                yield return new ValidationResult(@"NPI number must consist of 10 digits.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(TelephoneNumber))
            {
                IEnumerable<string> memberNames = new[] { "TelephoneNumber" };
                yield return new ValidationResult(@"Telephone number can not be empty.", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(TelephoneNumber) && !Regex.IsMatch(TelephoneNumber, @"^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:\(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9])\s*\)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$"))
            {
                IEnumerable<string> memberNames = new[] { "TelephoneNumber" };
                yield return new ValidationResult(@"Telephone number must be either 7, 10, or 11 digits.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                IEnumerable<string> memberNames = new[] { "EmailAddress" };
                yield return new ValidationResult(@"Email address can not be empty.", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(EmailAddress) && !Regex.IsMatch(EmailAddress, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                IEnumerable<string> memberNames = new[] { "EmailAddress" };
                yield return new ValidationResult(@"Email address must be a valid format.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(Address1))
            {
                IEnumerable<string> memberNames = new[] { "Address1" };
                yield return new ValidationResult(@"Address can not be empty.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(Address1) && !string.IsNullOrWhiteSpace(Address2))
            {
                IEnumerable<string> memberNames = new[] { "Address2" };
                yield return new ValidationResult(@"Use Address line 1 before using line 2.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(City))
            {
                IEnumerable<string> memberNames = new[] { "City" };
                yield return new ValidationResult(@"City can not be empty.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(State))
            {
                IEnumerable<string> memberNames = new[] { "State" };
                yield return new ValidationResult(@"State can not be empty.", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(State) && !Regex.IsMatch(State, @"^((A[LKSZR])|(C[AOT])|(D[EC])|(F[ML])|(G[AU])|(HI)|(I[DLNA])|(K[SY])|(LA)|(M[EHDAINSOT])|(N[EVHJMYCD])|(MP)|(O[HKR])|(P[WAR])|(RI)|(S[CD])|(T[NX])|(UT)|(V[TIA])|(W[AVIY]))$"))
            {
                IEnumerable<string> memberNames = new[] { "State" };
                yield return new ValidationResult(@"State must be a two letter abreviation of a valid US State or Territory.", memberNames);
            }
            if (string.IsNullOrWhiteSpace(ZipCode))
            {
                IEnumerable<string> memberNames = new[] { "ZipCode" };
                yield return new ValidationResult(@"ZipCode can not be empty.", memberNames);
            }
            if (!string.IsNullOrWhiteSpace(ZipCode) && !Regex.IsMatch(ZipCode, @"^\d{5}$|^\d{5}-\d{4}$"))
            {
                IEnumerable<string> memberNames = new[] { "ZipCode" };
                yield return new ValidationResult(@"ZipCode must be either 5 digits or 5+4 digits.", memberNames);
            }
        }
    }
}
