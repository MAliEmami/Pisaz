using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Clients
{
    public class VIPClientDTO
    {
        public bool IsVIP { get; set; }
        public DateTime VIPExpireTime { get; set; }
        public int Profit { get; set; }
    }
}