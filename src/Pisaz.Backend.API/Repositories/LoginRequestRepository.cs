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
            // var sql = $"SELECT * FROM Client WHERE PhoneNumber = @phoneNumber";

            // var parameters = new[]
            // {
            //     new SqlParameter("@phoneNumber", phoneNumber)
            // };
            // return  _db.Clients.FromSqlRaw(sql, parameters)
            //                    .FirstOrDefault();
             return _db.Clients.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }
    }
}