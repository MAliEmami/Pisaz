using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.PCpart
{
    public class GPU
    {
        public int ID { get; set; }
        public decimal ClockSpeed { get; set; }
        public int RamSize { get; set; }
        public int NumberOfFans { get; set; }
        public int Wattage { get; set; }
        public decimal Depth { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}