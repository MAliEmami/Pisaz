using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.Clients
{
    public class ClientUpdateDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}