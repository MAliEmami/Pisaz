using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.Cart
{
    public class IssuedFor
    {
        public required string TrackingCode { get; set; }
        public int ID { get; set; }
        public int CartNumber { get; set; }
        public int LockedNumber { get; set; }
    }
}