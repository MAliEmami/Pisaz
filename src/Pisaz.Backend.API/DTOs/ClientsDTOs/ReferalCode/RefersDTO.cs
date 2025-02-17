using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.ReferalCode
{
    public class RefersDTO
    {
        public int ReferalCode { get; set; }
        public int NumInvited { get; set; }
        public int NumDiscountGift { get; set; }
    }
}