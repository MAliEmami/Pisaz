using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.Clients.SignIn
{
    public class ClientSignInDTO
    {
        public required string PhoneNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}