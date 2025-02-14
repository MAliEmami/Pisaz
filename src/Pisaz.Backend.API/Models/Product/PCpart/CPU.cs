using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.PCpart
{
    public class CPU
    {
        public int ID { get; set; }
        public int MaxAddressableMemLimit { get; set; }
        public decimal BoostFrequency { get; set; }
        public decimal BaseFrequency { get; set; }
        public int NumOfCores { get; set; }
        public int NumOfThreads { get; set; }
        public required string Microarchitecture { get; set; }
        public required string Generation { get; set; }
        public decimal Wattage { get; set; }
    }
}