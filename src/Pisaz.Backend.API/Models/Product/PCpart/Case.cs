using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.PCpart
{
    public class Case
    {
        public int ID { get; set; }
        public int NumberOfFans { get; set; }
        public int FanSize { get; set; }
        public int Wattage { get; set; }
        public required string CaseType { get; set; }
        public required string Material { get; set; }
        public required string Color { get; set; }
        public decimal Depth { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
    }
}