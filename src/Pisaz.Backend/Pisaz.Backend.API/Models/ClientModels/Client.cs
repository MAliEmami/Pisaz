using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Models.ClientModels
{
    public class Client : GeneralModel
    {
        public required string PhoneNumber { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required decimal WalletBalance { get; set; }
        public string? ReferralCode { get; set; }
        public required DateTime SignUpDate { get; set; } = DateTime.Now;

    }
}