using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Discount;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class PurchaseHistoryService(PisazDB db) 
    : IQueryService<Products ,PurchaseHistoryDTO>
    {
        private readonly PisazDB _db = db;
        public async Task<IEnumerable<PurchaseHistoryDTO>> ListAsync(int id)
        {
            const string PurchaseHistoryQuery = @"
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


            var PurchaseHistoryList = await _db.Database
                                        .SqlQueryRaw<PurchaseHistoryDTO>(PurchaseHistoryQuery, new SqlParameter("@id", id))
                                        .ToListAsync();

            return PurchaseHistoryList
            .Select(ph => new PurchaseHistoryDTO
            {
                ProductList = ph.ProductList,
                TotalPrice = ph.TotalPrice,
                TransactionTime = ph.TransactionTime
            }).ToList();
        }
    }
}