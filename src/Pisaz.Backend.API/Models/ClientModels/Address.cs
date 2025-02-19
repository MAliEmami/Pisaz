using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.ClientModels
{
    public class Address
    {
        public int ID { get; set; }
        public required string Province { get; set; }
        public required string Remainder { get; set; }
    }
}