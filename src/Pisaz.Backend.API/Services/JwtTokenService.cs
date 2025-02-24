using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pisaz.Backend.API.Services
{
    public class JwtTokenService
    {
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(int userId)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret is missing in configuration.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var claims = new List<Claim>
        {
            new Claim("userId", userId.ToString()),
        };

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationInMinutes"] ?? "60"));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires, 
            NotBefore = now,
            SigningCredentials = credentials,
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public int GetUserId(HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirst("userId")?.Value;

        if (!int.TryParse(userId, out int clientId))
            throw new ArgumentException("Invalid userId format");

        return clientId;
    }

        
    }
}