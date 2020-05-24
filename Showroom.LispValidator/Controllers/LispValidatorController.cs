using Microsoft.AspNetCore.Mvc;
using Showroom.LispValidator.Models;
using Showroom.LispValidator.Services;
using Showroom.Shared.Filters;

namespace Showroom.ListValidator.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ActionResultFilter))]
    public class LispValidatorController : ControllerBase
    {
        private readonly ILispValidatorService LispValidatorService;
        public LispValidatorController(
            ILispValidatorService lispValidatorService) 
        {
            LispValidatorService = lispValidatorService;
        }

        [HttpGet]
        [Route("lisp")]
        [ServiceFilter(typeof(ActionExecuteFilter<LispValidatorRequest>))]
        public IActionResult Get([FromQuery] LispValidatorRequest request)
        {
            return ValidateLisp(request);
        }

        [HttpPost]
        [Route("lisp")]
        [ServiceFilter(typeof(ActionExecuteFilter<LispValidatorRequest>))]
        public IActionResult Post([FromBody] LispValidatorRequest request)
        {
            return ValidateLisp(request);
        }

        private IActionResult ValidateLisp(LispValidatorRequest request)
        {
            if (!LispValidatorService.ValidateLisp(request, out string message))
            {
                return BadRequest(new LispValidatorResponse()
                {
                    ValidLispString = false,
                    Message = message,
                    Request = request
                });
            }
            return Ok(new LispValidatorResponse()
            {
                ValidLispString = true,
                Message = message,
                Request = request
            }); ;
        }
    }
}
