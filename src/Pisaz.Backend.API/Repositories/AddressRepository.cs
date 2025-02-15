using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly PisazDB _db;

        public AddressRepository(PisazDB db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Address>> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Address WHERE ID = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return await _db.Addresses.FromSqlRaw(sql, parameters)
                                      .ToListAsync();
        }
        public async Task<int> AddAsync(Address entity)
        {
            const string sql = @"
                                INSERT INTO Client (Province, Remainder)
                                VALUES (@Province, @Remainder);";
            var parameters = new[]
            {
                new SqlParameter("@Province", entity.Province),
                new SqlParameter("@Remainder", entity.Remainder)
            };

            int id = await _db.Database.ExecuteSqlRawAsync(sql, parameters);

            return id;
        }
        public async Task<int> UpdateAsync(Address entity)
        {
            var sql = @"
                UPDATE Client 
                SET Province = @Province, 
                    Remainder = @Remainder, 
                WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@Province", entity.Province),
                new SqlParameter("@Remainder", entity.Remainder)
            };

            int id = await _db.Database.ExecuteSqlRawAsync(sql, parameters);
            return id;
        }
    }
}