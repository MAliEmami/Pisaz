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
    public class PurchasehistoryService(PisazDB db) 
    : IQueryService<DiscountCode ,DiscountCodeDTO>
    {
        private readonly PisazDB _db = db;
        public async Task<IEnumerable<DiscountCodeDTO>> ListAsync(int id)
        {
            const string DiscountCodeIsGoingToExpList = @"
                SELECT";


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