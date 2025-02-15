using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace Pisaz.Backend.API.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly PisazDB _db;

        public ClientRepository(PisazDB db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Client>> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Client WHERE ID = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return await _db.Clients.FromSqlRaw(sql, parameters)
                                    .ToListAsync();
        }

        public async Task<int> AddAsync(Client entity)
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

            return 1;
        }

        public async Task<int> UpdateAsync(Client entity)
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
            return id;
        }
    }
}