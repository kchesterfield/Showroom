using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Showroom.User.Models;
using Showroom.Shared.Filters;
using Showroom.User.Data;

namespace Showroom.User.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ActionResultFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserData UserData;

        public UserController(IUserData userData) 
        {
            UserData = userData;
        }

        [HttpGet]
        [Route("api/user")]
        [ServiceFilter(typeof(ActionExecuteFilter<UserSearch>))]
        public IActionResult Get([FromQuery] UserSearch parameters)
        {
            return BadRequest("Search has not been implemented, please try back later.");
        }

        [HttpPost]
        [Route("api/user")]
        [ServiceFilter(typeof(ActionExecuteFilter<UserResource>))]
        public IActionResult Post([FromBody] UserResource request)
        {
            return Ok(UserData.CreateUser(request));
        }
    }
}
