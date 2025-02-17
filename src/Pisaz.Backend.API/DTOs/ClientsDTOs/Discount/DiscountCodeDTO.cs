using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Discount
{
    public class DiscountCodeDTO
    {
        public int DiscountCodeIsGoingToExp { get; set; }
        public int Amount { get; set; }
        public int? DiscountLimit { get; set; }
        public Int16 UsageCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}