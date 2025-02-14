using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Discount
{
    public class PrivateCode
    {
        public int Code { get; set; }
        public int ID { get; set; }
        public DateTime UsageTimestamp { get; set; }
    }
}