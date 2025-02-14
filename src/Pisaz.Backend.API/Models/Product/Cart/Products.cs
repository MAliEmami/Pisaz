using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.Cart
{
    public class Products
    {
        public int ID { get; set; }
        public required string Category { get; set; }
        public byte[]? ProductImage { get; set; }
        public int CurrentPrice { get; set; }
        public int StockCount { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
    }
}