using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly IDbConnection _dbConnection;

        public ClientRepository(IUnitOfWork unitOfWork)
        {
            _dbConnection = unitOfWork.Connection;
        }

        public async Task<IEnumerable<Client>> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Client WHERE ID = @Id";
            return await _dbConnection.QueryAsync<Client>(sql, new { ID = id });
        }

        public async Task<int> AddAsync(Client entity)
        {
            var sql = @"
                INSERT INTO Client (PhoneNumber, FirstName, LastName, WalletBalance, ReferralCode, SignUpDate)
                VALUES (@PhoneNumber, @FirstName, @LastName, @WalletBalance, @ReferralCode, @SignUpDate);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _dbConnection.QuerySingleAsync<int>(sql, entity);
            return id;
        }

        public async Task<int> UpdateAsync(Client entity)
        {
            var sql = @"
                UPDATE Client 
                SET PhoneNumber = @PhoneNumber, 
                    FirstName = @FirstName, 
                    LastName = @LastName, 
                    WalletBalance = @WalletBalance, 
                    ReferralCode = @ReferralCode, 
                    SignUpDate = @SignUpDate
                WHERE Id = @Id";

            var affectedRows = await _dbConnection.ExecuteAsync(sql, entity);
            return affectedRows;
        }

        public async Task<int> RemoveAsync(Client entity)
        {
            var sql = "DELETE FROM Client WHERE ID = @Id";
            var affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = entity.ID });
            return affectedRows;
        }

        // Uncomment if needed
        // public async Task<int> SaveAsync()
        // {
        //     // Assuming SaveAsync is meant to commit the transaction
        //     // This method might need to be adjusted based on how your UnitOfWork is implemented
        //     return await _unitOfWork.SaveChangesAsync();
        // }
    }
}