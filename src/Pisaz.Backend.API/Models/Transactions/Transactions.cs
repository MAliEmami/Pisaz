using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Transactions
{
    public class Transactions
    {
        public required string TrackingCode { get; set; }
        public required string TransactionStatus { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}