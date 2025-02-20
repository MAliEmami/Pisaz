using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs;
using System.Threading.Tasks;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly PisazDB _context;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(PisazDB context, JwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        if (string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return BadRequest("Phone number is required.");
        }

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == request.PhoneNumber);
        if (client == null)
        {
            return Unauthorized("User not found.");
        }

        var token = _jwtTokenService.GenerateToken(client.PhoneNumber);
        return Ok(new { Token = token });
    }
}
