using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Showroom.Enrollment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Showroom.Enrollment.Services
{
    public interface IEnrollmentService
    {
        public EnrollmentResponse Transform(EnrollmentRequest request);
    }

    public class EnrollmentService : IEnrollmentService
    {
        public EnrollmentResponse Transform(EnrollmentRequest request)
        {
            string csv = request.ToCsvString();

            if (!ValidateCsv(csv))
            {
                return new EnrollmentResponse()
                {
                    Status = EnrollmentResponseStatus.Error,
                    Message = $"The CSV supplied does not have the correct format. Ensure that no quotes are present and 3 columns are present."
                };
            }

            // Transform
            List<CsvLine> enrollments = new List<CsvLine>();
            foreach(var enrollment in Regex.Split(csv, @"\\n"))
            {
                if (string.IsNullOrWhiteSpace(enrollment)) { break; }

                string[] columns = enrollment.Split(',');

                enrollments.Add(new CsvLine()
                {
                    UserId = columns[0].Trim(),
                    FirstAndLastName = columns[1].Trim(),
                    Version = int.Parse(columns[2].Trim()),
                    InsuranceCompany = columns[3].Trim()
                });
            }

            // Remove UserId duplicates for the same Insurance Comany and take highest Version number
            var sorted = enrollments.
                GroupBy(g => new { g.UserId, g.InsuranceCompany }).
                Select(g => g.OrderByDescending(y => y.Version).First());

            // Sort values into Enrollment Resouce
            var response = new List<EnrollmentResource>();
            foreach (var org in sorted.Select(x => x.InsuranceCompany).Distinct())
            {
                var enrollees = new List<Enrollee>();
                foreach (var user in sorted.Where(i => i.InsuranceCompany == org).ToList())
                {
                    enrollees.Add(new Enrollee()
                    {
                        UserId = user.UserId,
                        FirstAndLastName = user.FirstAndLastName,
                        Version = user.Version
                    });
                }
                response.Add(new EnrollmentResource()
                {
                    InsuranceCompany = org,
                    Enrollees = enrollees.OrderBy(x => x.FirstAndLastName).ToList()
                });
            }

            return new EnrollmentResponse()
            {
                Status = EnrollmentResponseStatus.Success,
                Message = "Successful transformation.",
                Enrollments = response
            };
        }

        private class CsvLine
        {
            public string UserId { get; set; }
            public string FirstAndLastName { get; set; }
            public int Version { get; set; }
            public string InsuranceCompany { get; set; }
        }

        private bool ValidateCsv(string csv)
        {
            foreach (var line in Regex.Split(csv, @"\\n"))
            {
                if (string.IsNullOrWhiteSpace(line)) { break; }

                // Quotes are not currently allowed
                if (Regex.IsMatch(line, "\""))
                {
                    return false;
                }
                // Count number of columns
                if (Regex.Matches(line, @",").Count != 3)
                {
                    return false;
                }
                // Check for empty values
                foreach(var column in line.Split(','))
                {
                    if (string.IsNullOrWhiteSpace(column))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
