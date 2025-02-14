using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.PCpart
{
    public class Cooler
    {
        public int ID { get; set; }
        public int MaxRotationalSpeed { get; set; }
        public decimal FanSize { get; set; }
        public string? CoolingMethod { get; set; }
        public int Wattage { get; set; }
        public decimal Depth { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}