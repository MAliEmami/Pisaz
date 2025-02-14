using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.PCpart
{
    public class Motherboard
    {
        public int ID { get; set; }
        public required string Chipset { get; set; }
        public int NumberOfMemorySlots { get; set; }
        public required string MemorySpeedRange { get; set; }
        public int Wattage { get; set; }
        public decimal Depth { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}