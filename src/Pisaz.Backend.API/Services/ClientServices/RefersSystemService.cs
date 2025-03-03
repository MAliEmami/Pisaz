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

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class RefersSystemService(PisazDB db) : IQueryService<Refers, RefersDTO>
    {
        private readonly PisazDB _db = db;
        public async Task<IEnumerable<RefersDTO>> ListAsync(int id)
        {
            const string RefersInfoQuery = @"
                    WITH ReferralChain AS (
                        SELECT 
                            Referee, 
                            Referrer, 
                            1 AS Depth
                        FROM 
                            Refers
                        WHERE 
                            Referrer = @id
                        UNION ALL
                        SELECT 
                            R.Referee, 
                            R.Referrer, 
                            RC.Depth + 1 AS Depth
                        FROM 
                            Refers R
                        INNER JOIN 
                            ReferralChain RC ON R.Referrer = RC.Referee
                    )
                    SELECT 
                        (SELECT ReferralCode FROM Client WHERE ID = @id) AS ReferalCode,
                        (SELECT COUNT(*) FROM Refers WHERE Referrer = @id) AS NumInvited,
                        MAX(Depth) AS NumDiscountGift
                    FROM 
                        ReferralChain";

            var RefersInfoList = await _db.Database
                                        .SqlQueryRaw<RefersDTO>(RefersInfoQuery, new SqlParameter("@id", id))
                                        .ToListAsync();

            return RefersInfoList
            .Select(r => new RefersDTO
            {
                ReferalCode = r.ReferalCode,
                NumInvited = r.NumInvited,
                NumDiscountGift = r.NumDiscountGift

            }).ToList();
        }
                        
        
    }
}