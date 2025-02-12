using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.DTOs.Clients
{
    public class ClientDashboardDTOs
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
        public required decimal WalletBalance { get; set; }
        public string? ReferralCode { get; set; }
        public required DateTime SignUpDate { get; set; }

        // VIPClient
        // public required string VIPClientID { get; set; }
        // public int NumInvited { get; set; }
        // public int NumDiscountGift { get; set; }
    }
}