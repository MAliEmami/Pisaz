using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.ReferalCode;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class RefersSystemService(RefersSystemRepository refersSystem) : IQueryService<Refers, RefersDTO>
    {
        private readonly RefersSystemRepository _refersSystem = refersSystem;
        public async Task<IEnumerable<RefersDTO>> ListAsync(int id)
        {
            var refers = await _refersSystem.GetByIdAsync(id);
            
            if (refers == null) 
            {
                return new List<RefersDTO>();
            }

            return refers
            .Select(r => new RefersDTO
            {
                ReferalCode = r.ReferalCode,
                NumInvited = r.NumInvited,
                NumDiscountGift = r.NumDiscountGift

            }).ToList();
        }
                        
        
    }
}