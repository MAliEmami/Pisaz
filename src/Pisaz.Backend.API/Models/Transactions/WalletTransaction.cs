using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Transactions
{
    public class WalletTransaction
    {
        public required string TrackingCode { get; set; }
    }
}