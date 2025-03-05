using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;

namespace Pisaz.Backend.API.Repositories
{
    public class PurchasehistoryRepository(PisazDB db) //: IQueryRepository<ShoppingCart>
    {
        private readonly PisazDB _db = db;
        public async Task<List<PurchaseHistoryDTO?>> GetByIdAsync(int id)
        {
            const string DiscountCodeIsGoingToExpList = @"
                SELECT TOP 5
                    STRING_AGG(P.Category + ' ' + P.Brand + ' ' + P.Model, ', ') AS ProductList,
                    SUM(A.CartPrice * A.Quantity) AS TotalPrice,
                    T.TransactionTime
                FROM 
                    AddedTo A
                JOIN 
                    Products P ON A.ProductID = P.ID
                JOIN 
                    IssuedFor I  ON A.ID = I.ID 
                                AND A.CartNumber = I.CartNumber 
                                AND A.LockedNumber = I.LockedNumber
                JOIN 
                    Transactions T ON I.TrackingCode = T.TrackingCode
                JOIN 
                    LockedShoppingCart LSC  ON A.ID = LSC.ID 
                                            AND A.CartNumber = LSC.CartNumber 
                                            AND A.LockedNumber = LSC.LockedNumber
                WHERE 
                    T.TransactionStatus = 'Successful'
                    AND
                    LSC.ID = @id
                GROUP BY
                    A.ID, A.CartNumber, A.LockedNumber, I.TrackingCode, T.TransactionStatus, T.TransactionTime
                ORDER BY 
                    T.TransactionTime DESC;";
                    
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return (await _db.Database.SqlQueryRaw<PurchaseHistoryDTO>(DiscountCodeIsGoingToExpList, parameters)
                                    .ToListAsync())
                                    .Cast<PurchaseHistoryDTO?>()
                                    .ToList();
                                    
        }
    
        
    }
}