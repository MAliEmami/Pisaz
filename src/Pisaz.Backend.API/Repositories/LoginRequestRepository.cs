using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Repositories
{
    public class LoginRequestRepository(PisazDB db)
    {
        private readonly PisazDB _db = db;

        public async Task<Client?> GetClientByPhoneNumberAsync(string phoneNumber)
        {
            return await _db.Clients.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }
    }
}