using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Clients;
using Pisaz.Backend.API.Interfaces;

namespace Pisaz.Backend.API.Repositories
{
    public class VIPClientRepository(PisazDB db)
    {
        private readonly PisazDB _db = db;
        public async Task<List<VIPClientDTO?>> GetByIdAsync(int id)
        {
            const string RefersInfoQuery = @"
                SELECT 
                    COALESCE(SUM(ADT.CartPrice) * 0.15, 0) AS VIP_Profit,
                    DATEDIFF(day, CURRENT_TIMESTAMP, SubsctiptionExpirationTime) AS DaysRemaining
                FROM 
                    VIPClient VC
                JOIN 
                    IssuedFor ISF ON VC.ID = ISF.ID
                JOIN 
                    Transactions T ON ISF.TrackingCode = T.TrackingCode
                JOIN 
                    AddedTo ADT ON ISF.ID = ADT.ID 
                    AND ISF.CartNumber = ADT.CartNumber 
                    AND ISF.LockedNumber = ADT.LockedNumber
                WHERE VC.ID = @id
                    AND T.TransactionStatus = 'Successful'
                    AND T.TransactionTime >= DATEADD(DAY, -DAY(GETDATE()) + 1, CAST(GETDATE() AS DATE))
                    AND T.TransactionTime <= GETDATE()
                    AND VC.SubsctiptionExpirationTime >= T.TransactionTime
                GROUP BY 
                    VC.ID, VC.SubsctiptionExpirationTime;";
                    
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };

            return (await _db.Database.SqlQueryRaw<VIPClientDTO>(RefersInfoQuery, parameters)
                                    .ToListAsync())
                                    .Cast<VIPClientDTO?>()
                                    .ToList();
        }
    }
}