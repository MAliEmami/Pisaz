using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard
{
    public class AddressDTO
    {
        [JsonIgnore]
        public int ID { get; set; }
        public required string Province { get; set; }
        public required string Remainder { get; set; }
    }
}