using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Discount;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class DiscountService(PisazDB db) 
    : IListService<DiscountCode ,DiscountCodeDTO>
    {
        private readonly PisazDB _db = db;
        public async Task<IEnumerable<DiscountCodeDTO>> ListAsync(int id)
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


            var clientInfoList = await _db.Database
                                        .SqlQueryRaw<DiscountCodeDTO>(DiscountCodeIsGoingToExpList, new SqlParameter("@id", id))
                                        .ToListAsync();

            return clientInfoList
            .Select(d => new DiscountCodeDTO
            {
                DiscountCodeIsGoingToExp = d.DiscountCodeIsGoingToExp,
                Amount = d.Amount,
                DiscountLimit = d.DiscountLimit,
                UsageCount = d.UsageCount,
                ExpirationDate = d.ExpirationDate
            }).ToList();
        }
    }
}