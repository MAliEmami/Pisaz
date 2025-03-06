using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.ProductsDTOs
{
    public class CompatibleWithDTO
    {
        public required string Category { get; set; }
        public int CurrentPrice { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int StockCount { get; set; }
    }
}