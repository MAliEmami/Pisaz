using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Models.ClientModels;
using Microsoft.Data.SqlClient;

namespace Pisaz.Backend.API.Repositories
{
    public class LoginRequestRepository(PisazDB db)
    {
        private readonly PisazDB _db = db;

        public Client? GetClientByPhoneNumber(string phoneNumber)
        {
            var clientWithPhoneNumberQuery = $"SELECT * FROM Client WHERE PhoneNumber = @phoneNumber";

            var parameters = new[]
            {
                new SqlParameter("@phoneNumber", phoneNumber)
            };
            return  _db.Clients.FromSqlRaw(clientWithPhoneNumberQuery, parameters)
                               .FirstOrDefault();
        }
        public bool IsVIP(int id)
        {
            var isVIPQuery = $"SELECT 1 AS IsVIP FROM VIPClient WHERE ID = @id";

            var parameters = new[]
            {
                new SqlParameter("@id", id)
            };

            return _db.VIPClients.FromSqlRaw(isVIPQuery, parameters)
                                       .Any();
        }
    }
}