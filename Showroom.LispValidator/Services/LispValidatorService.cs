using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Showroom.LispValidator.Models;
using System.Text.RegularExpressions;


namespace Showroom.LispValidator.Services
{
    public interface ILispValidatorService
    {
        bool ValidateLisp(LispValidatorRequest request, out string message);
        bool ValidateLisp(LispValidatorRequest request);
        bool ValidateLisp(string value, out string message);
        bool ValidateLisp(string value);
    }

    public class LispValidatorService : ILispValidatorService
    {
        public bool ValidateLisp(LispValidatorRequest request, out string message)
        {
            return ValidateLisp(request.RequestToString(), out message);
        }

        public bool ValidateLisp(LispValidatorRequest request)
        {
            return ValidateLisp(request.RequestToString(), out _);
        }

        public bool ValidateLisp(string value, out string message)
        {
            return ValidateParentheses(value, out message);
        }

        public bool ValidateLisp(string value)
        {
            return ValidateParentheses(value, out _);
        }

        private bool ValidateParentheses(string value, out string message)
        {
            // Remove comments
            value = Regex.Replace(value, @";(.*?)((\\n)|$)", "");

            // Remove quotes within strings
            if (Regex.Matches(value, @"(?<!\\)(\\\u0022)(.*?)(?<!\\)(\\\u0022)").Count % 2 != 0)
            {
                message = "Error: There is an odd number of string quotation marks.";
                return false;
            }
            value = Regex.Replace(value, @"(?<!\\)(\\\u0022)(.*?)(?<!\\)(\\\u0022)", "");

            // Obtain stack
            var values = value.ToCharArray().AsEnumerable().Where(x => x.Equals('(') || x.Equals(')'));

            // Parentheses
            int found = 0;
            foreach( var v in values )
            {
                _ = v.Equals('(') ? found++ : found--;

                if (found < 0)
                {
                    message = "Error: An unexpected parenthesis ')' was found.";
                    return false;
                }
            }

            if (found > 0)
            {
                message = "Error: An unexpected parenthesis '(' was found.";
                return false;
            }

            message = "The LISP code had no parentheses errors.";
            return true;
        }
    }
}
