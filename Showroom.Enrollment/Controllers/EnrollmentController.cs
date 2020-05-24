using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Showroom.Enrollment.Models;
using Showroom.Enrollment.Services;
using Showroom.Shared.Filters;

namespace Showroom.Enrollment.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ActionExecuteFilter<EnrollmentRequest>))]
    [ServiceFilter(typeof(ActionResultFilter))]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService EnrollmentService;

        public EnrollmentController(
            IEnrollmentService enrollmentService) 
        {
            EnrollmentService = enrollmentService;
        }

        [HttpGet("enrollment/transform")]
        public IActionResult GetEnrollment([FromQuery] EnrollmentRequest request)
        {
            return TransformEnrollment(request);
        }

        [HttpPost("enrollment/transform")]
        public IActionResult Post([FromBody] EnrollmentRequest request)
        {
            return TransformEnrollment(request);
        }

        private IActionResult TransformEnrollment(EnrollmentRequest request)
        {
            var response = EnrollmentService.Transform(request);
            if (response.Status != EnrollmentResponseStatus.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
