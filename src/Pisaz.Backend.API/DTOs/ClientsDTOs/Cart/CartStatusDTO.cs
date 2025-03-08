using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Cart
{
    public class CartStatusDTO
    {
        public required Byte CartNumber { get; set; }
        public required string CartStatus { get; set; }
    }
}