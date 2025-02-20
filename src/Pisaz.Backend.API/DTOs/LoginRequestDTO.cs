using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs
{
    public class LoginRequestDTO
    {
        public required string PhoneNumber { get; set; }
    }
    public class LoginResponseDTO
    {
        public required string Token { get; set; }
    }
}