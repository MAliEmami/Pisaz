using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Models.ClientModels
{
    public class VIPClient
    {
        public int Id { get; set; } // its should be remove
        public required Client Client { get; set; }
        public required DateTime SubscriptionExpirationTime { get; set; }
        
    }
}