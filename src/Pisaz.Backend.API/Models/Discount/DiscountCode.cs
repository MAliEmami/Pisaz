using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Discount
{
    public class DiscountCode
    {
        public int Code { get; set; }
        public int Amount { get; set; }
        public int DiscountLimit { get; set; }
        public int UsageCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}