using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.Cart
{
    public class AppliedTo
    {
        public int ID { get; set; }
        public int CartNumber { get; set; }
        public int LockedNumber { get; set; }
        public int Code { get; set; }
        public DateTime ApplyTimestamp { get; set; }
    }
}