using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Showroom.Enrollment.Models
{
    public enum EnrollmentResponseStatus
    {
        Success,
        Error,
        Empty
    }

    public class EnrollmentResponse
    {
        [JsonIgnore]
        public EnrollmentResponseStatus Status { get; set; }

        public string StatusDesc { get { return Status.ToString(); } }

        public string Message { get; set; }

        public List<EnrollmentResource> Enrollments { get; set; }
    }
}
