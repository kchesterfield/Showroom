using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Showroom.LispValidator.Models
{
    public class LispValidatorResponse
    {
        public LispValidatorRequest Request { get; set; }
        public bool ValidLispString { get; set; }
        public string Message { get; set; }
    }
}
