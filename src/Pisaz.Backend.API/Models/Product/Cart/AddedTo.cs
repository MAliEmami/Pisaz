using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.Product.Cart
{
    public class AddedTo
    {
        public int ID { get; set; }
        public int CartNumber { get; set; }
        public int LockedNumber { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int CartPrice { get; set; }
    }
}