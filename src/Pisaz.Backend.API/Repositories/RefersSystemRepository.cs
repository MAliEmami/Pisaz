using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.ReferalCode;

namespace Pisaz.Backend.API.Repositories
{
    public class RefersSystemRepository(PisazDB db)
    {
        private readonly PisazDB _db = db;
        public async Task<List<RefersDTO?>> GetByIdAsync(int id)
        {
            const string RefersInfoQuery = @"
                    WITH ReferralChain AS (
                        SELECT 
                            Referee, 
                            Referrer
                        FROM 
                            Refers
                        WHERE 
                            Referrer = @id
                        UNION ALL
                        SELECT 
                            R.Referee, 
                            R.Referrer
                        FROM 
                            Refers R
                        INNER JOIN 
                            ReferralChain RC ON R.Referrer = RC.Referee
                    )
                    SELECT 
                        (SELECT ReferralCode FROM Client WHERE ID = @id) AS ReferalCode,
                        (SELECT COUNT(*) FROM Refers WHERE Referrer = @id) AS NumInvited,
                        COUNT(*) AS NumDiscountGift
                    FROM 
                        ReferralChain";

            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return (await _db.Database.SqlQueryRaw<RefersDTO>(RefersInfoQuery, parameters)
                                    .ToListAsync())
                                    .Cast<RefersDTO?>()
                                    .ToList();
        }
    }
}