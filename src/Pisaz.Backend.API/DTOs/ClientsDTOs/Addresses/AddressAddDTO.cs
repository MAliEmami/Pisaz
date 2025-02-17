using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard
{
    public class AddressAddDTO
    {
        public int ID { get; set; }
        public required string Province { get; set; }
        public required string Remainder { get; set; }
    }
}