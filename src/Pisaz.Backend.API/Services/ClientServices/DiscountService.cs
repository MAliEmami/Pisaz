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
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class DiscountService(DiscountRepository discount) 
    : IQueryService<DiscountCode ,DiscountCodeDTO>
    {
         private readonly DiscountRepository _discount = discount;

        public async Task<IEnumerable<DiscountCodeDTO>> ListAsync(int id)
        {
            var cartStatus = await _discount.GetByIdAsync(id);
            
            if (cartStatus == null) 
            {
                return new List<DiscountCodeDTO>();
            }

            return cartStatus
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