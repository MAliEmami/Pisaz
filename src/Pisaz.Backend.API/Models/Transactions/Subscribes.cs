using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Transactions
{
    public class Subscribes
    {
        public required string TrackingCode { get; set; }
        public int ID { get; set; }
    }
}