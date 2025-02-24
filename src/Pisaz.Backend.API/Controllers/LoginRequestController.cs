using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs;
using Pisaz.Backend.API.Services.ClientServices;
using System.Threading.Tasks;


[ApiController]
[Route("Auth")]
public class LoginRequestController(LoginRequestService loginRequestService) : ControllerBase
{
    private readonly LoginRequestService _loginRequestService = loginRequestService;

    //[Authorize]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        try
        {
            var response = await _loginRequestService.AuthenticateAsync(request);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
