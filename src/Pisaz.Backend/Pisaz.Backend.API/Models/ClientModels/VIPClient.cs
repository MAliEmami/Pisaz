using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Models.ClientModels
{
    public class VIPClient : GeneralModel
    {
        public required Client Client { get; set; }
        public required DateTime SubscriptionExpirationTime { get; set; }
        
    }
}