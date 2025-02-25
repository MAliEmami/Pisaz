using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.Services;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Auth/v1")]
    public class LoginRequestApiController : ControllerBase
    {
        private readonly AuthService _authService;

        public LoginRequestApiController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] string phoneNumber)
        {
            var token = _authService.Authenticate(phoneNumber);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}