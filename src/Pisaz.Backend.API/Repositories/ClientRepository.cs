using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Pisaz.Backend.API.DTOs.Clients;


namespace Pisaz.Backend.API.Repositories
{
    public class ClientRepository(PisazDB db) //: IRepository<Client>
    {
        private readonly PisazDB _db = db;

        public async Task<List<ClientDTO?>> GetByIdAsync(int id)
        {
            const string clientInfoListQuery = @"
                SELECT 
                    FirstName,     
                    LastName,     
                    PhoneNumber,   
                    WalletBalance, 
                    SignupDate
                FROM Client C 
                WHERE C.ID = @id";


            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return (await _db.Database.SqlQueryRaw<ClientDTO>(clientInfoListQuery, parameters)
                                    .ToListAsync())
                                    .Cast<ClientDTO?>()
                                    .ToList();
        }

        public async Task<Client> AddAsync(Client entity)
        {
            const string sql = @"
                                INSERT INTO Client (PhoneNumber, FirstName, LastName)
                                VALUES (@PhoneNumber, @FirstName, @LastName);";
            var parameters = new[]
            {
                new SqlParameter("@PhoneNumber", entity.PhoneNumber),
                new SqlParameter("@FirstName", entity.FirstName),
                new SqlParameter("@LastName", entity.LastName)
            };

            await _db.Database.ExecuteSqlRawAsync(sql, parameters);

            return entity;
        }

        public async Task<Client?> UpdateAsync(Client entity)
        {
            var sql = @"
                UPDATE Client 
                SET PhoneNumber = @PhoneNumber, 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@PhoneNumber", entity.PhoneNumber),
                new SqlParameter("@FirstName", entity.FirstName),
                new SqlParameter("@LastName", entity.LastName)
            };

            int id = await _db.Database.ExecuteSqlRawAsync(sql, parameters);
            return entity;
        }
    }
}