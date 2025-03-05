using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ClientsDTOs.Cart
{
    public class PurchaseHistoryDTO
    {
        public required string ProductList { get; set; }
        public int TotalPrice { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}