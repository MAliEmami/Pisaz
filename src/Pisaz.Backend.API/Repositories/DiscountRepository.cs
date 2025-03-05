using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;

namespace Pisaz.Backend.API.Repositories
{
    public class DiscountRepository(PisazDB db) //: IQueryRepository<ShoppingCart>
    {
        private readonly PisazDB _db = db;
        public async Task<List<DiscountCodeDTO?>> GetByIdAsync(int id)
        {
            const string DiscountCodeIsGoingToExpList = @"
                SELECT 
                    D.Code AS DiscountCodeIsGoingToExp,
                    D.Amount AS Amount,
                    D.DiscountLimit AS DiscountLimit,
                    D.UsageCount AS UsageCount,
                    D.ExpirationDate AS ExpirationDate
                FROM DiscountCode D JOIN PrivateCode P ON D.Code = P.Code
                WHERE P.ID = @id 
                AND ExpirationDate BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE())";
                    
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return (await _db.Database.SqlQueryRaw<DiscountCodeDTO>(DiscountCodeIsGoingToExpList, parameters)
                                    .ToListAsync())
                                    .Cast<DiscountCodeDTO?>()
                                    .ToList();
                                    
        }
    }
}