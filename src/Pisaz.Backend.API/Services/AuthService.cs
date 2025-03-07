using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services
{
    public class AuthService
    {
        private readonly LoginRequestRepository _loginRequestRepository;
        private readonly IConfiguration _configuration;
        public AuthService(LoginRequestRepository loginRequestRepository, IConfiguration configuration)
        {
            _loginRequestRepository = loginRequestRepository;
            _configuration = configuration;
        }

        public async Task<string?> Authenticate(string phoneNumber)
        {
            var client = _loginRequestRepository.GetClientByPhoneNumber(phoneNumber);
            var amIVIP = _loginRequestRepository.IsVIP(client.ID);

            Console.WriteLine("am i vip : ");
            Console.WriteLine(amIVIP);
            Console.WriteLine("am i vip : ");
            
            if (client == null)
            {
                return null;
            }

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("ClientPhoneNumber", phoneNumber),
                    new Claim("IsVIP", amIVIP.ToString()),
                    new Claim("ClientID", client.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // public bool GetUserVIPStatus(HttpContext httpContext)
        // {
        //     var userVIPClaim = httpContext.User.FindFirst("isUserVIP")?.Value;

        //     ArgumentNullException.ThrowIfNull(userVIPClaim, nameof(userVIPClaim));
            
        //     var isUserVIP = ConvertUserVIPStatusToBool(userVIPClaim);

        //     return isUserVIP;
        // }
    }
}