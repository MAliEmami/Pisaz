using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.Cart
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public int CartNumber { get; set; }
        public required string CartStatus { get; set; }
    }
}