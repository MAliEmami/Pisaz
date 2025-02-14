using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.PCpart
{
    public class HDD
    {
        public int ID { get; set; }
        public int RotationalSpeed { get; set; }
        public int Wattage { get; set; }
        public int Capacity { get; set; }
        public decimal Depth { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}