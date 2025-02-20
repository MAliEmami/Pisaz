using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs;
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class LoginRequestService(LoginRequestRepository loginRequestRepository, JwtTokenService jwtTokenService)
    {
        private readonly LoginRequestRepository _loginRequestRepository = loginRequestRepository;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                throw new ArgumentException("Phone number is required.");
            }

            var client = await _loginRequestRepository.GetClientByPhoneNumberAsync(request.PhoneNumber);
            if (client == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }

            var token = _jwtTokenService.GenerateToken(client.PhoneNumber);
            return new LoginResponseDTO { Token = token };
        }
    }
}