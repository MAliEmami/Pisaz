using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Repositories
{
    public class AddressRepository
    : IQueryRepository<Address>, ICommandRepository<Address>
    {
        private readonly PisazDB _db;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(PisazDB db, ILogger<AddressRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<List<Address?>> GetByIdAsync(int id)
        {
            const string sql = @"SELECT * FROM Addresses WHERE ID = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return (await _db.Addresses.FromSqlRaw(sql, parameters).ToListAsync())
                .Cast<Address?>().ToList();
        }
        public async Task<Address> AddAsync(Address entity)
        {
            try
            {
                // const string checkSql = @"
                //         SELECT 
                //             COUNT(1) 
                //         FROM 
                //             Address 
                //         WHERE 
                //             ID = @id AND Province = @province AND Remainder = @remainder";
                // var checkParameters = new[]
                // {
                //     new SqlParameter("@id", entity.ID),
                //     new SqlParameter("@province", entity.Province),
                //     new SqlParameter("@remainder", entity.Remainder)
                // };

                // var exists = _db.Addresses.FromSqlRaw(checkSql, checkParameters).Any();

                // if (exists)
                // {
                //     throw new Exception("An address already exists.");
                // }

                const string insertSql = @"
                                    INSERT INTO Addresses (ID, Province, Remainder)
                                    VALUES (@ID, @Province, @Remainder);";

                var parameters = new[]
                {
                    new SqlParameter("@ID", entity.ID),
                    new SqlParameter("@Province", entity.Province),
                    new SqlParameter("@Remainder", entity.Remainder)
                };

                await _db.Database.ExecuteSqlRawAsync(insertSql, parameters);

                return entity;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<Address?> UpdateAsync(Address entity)
        {
            var sql = @"
                UPDATE Addresses 
                SET Province = @Province, 
                    Remainder = @Remainder, 
                WHERE Id = @Id";

            var parameters = new[]
            {
                new SqlParameter("@Province", entity.Province),
                new SqlParameter("@Remainder", entity.Remainder)
            };

            int id = await _db.Database.ExecuteSqlRawAsync(sql, parameters);
            return entity;
        }
    }
}