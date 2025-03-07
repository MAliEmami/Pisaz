using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.ClientModels
{
    public class Address
    {
        [JsonIgnore]
        public int ID { get; set; }
        
        public required string Province { get; set; }
        public required string Remainder { get; set; }
    }
}