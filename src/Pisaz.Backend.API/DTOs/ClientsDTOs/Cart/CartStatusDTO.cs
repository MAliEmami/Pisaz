using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Cart
{
    public class CartStatusDTO
    {
        public Int16 CartNumber;
        public required string CartStatus { get; set; }
        public int NumAvailableCart { get; set; }
    }
}