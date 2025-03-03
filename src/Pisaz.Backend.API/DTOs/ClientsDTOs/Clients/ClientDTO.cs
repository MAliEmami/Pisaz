using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.DTOs.Clients
{
    public class ClientDTO
    {
        public required string PhoneNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required decimal WalletBalance { get; set; }
        public string? ReferralCode { get; set; }
        public required DateTime SignupDate { get; set; }
        public required string Province { get; set; }
        public required string Remainder { get; set; }
    }
}