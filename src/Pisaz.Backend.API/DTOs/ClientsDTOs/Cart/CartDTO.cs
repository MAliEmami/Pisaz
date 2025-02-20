using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Cart
{
    public class CartDTO
    {
        public required string Status { get; set; }
        public int NumAvailableCart { get; set; }
    }
}