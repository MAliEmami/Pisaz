using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ProductsDTOs;

namespace Pisaz.Backend.API.Repositories
{
    public class CompatibleWithRepository(PisazDB db)
    {
        private readonly PisazDB _db = db;
        public async Task<List<CompatibleWithDTO?>> GetByIdAsync(int id)
        {
            const string CompatibleWithQuery = @"
                SELECT 
                    P.Category,
                    P.CurrentPrice, 
                    P.Brand, 
                    P.Model, 
                    P.StockCount
                FROM (
                    -- CPU-Cooler compatibility
                    SELECT CoolerID AS CompatibleID FROM CcSlotCompatibleWith WHERE CPUID = @id
                    UNION
                    SELECT CPUID FROM CcSlotCompatibleWith WHERE CoolerID = @id
                    
                    UNION ALL
                    
                    -- CPU-Motherboard compatibility
                    SELECT MotherboardID FROM McSlotCompatibleWith WHERE CPUID = @id
                    UNION
                    SELECT CPUID FROM McSlotCompatibleWith WHERE MotherboardID = @id
                    
                    UNION ALL
                    
                    -- RAM-Motherboard compatibility
                    SELECT MotherboardID FROM RmSlotCompatibleWith WHERE RAMID = @id
                    UNION
                    SELECT RAMID FROM RmSlotCompatibleWith WHERE MotherboardID = @id
                    
                    UNION ALL
                    
                    -- GPU-PowerSupply compatibility
                    SELECT PowerID FROM ConnectorCompatibleWith WHERE GPUID = @id
                    UNION
                    SELECT GPUID FROM ConnectorCompatibleWith WHERE PowerID = @id
                    
                    UNION ALL
                    
                    -- SSD-Motherboard compatibility
                    SELECT MotherboardID FROM SmSlotCompatibleWith WHERE SSDID = @id
                    UNION
                    SELECT SSDID FROM SmSlotCompatibleWith WHERE MotherboardID = @id
                    
                    UNION ALL
                    
                    -- GPU-Motherboard compatibility
                    SELECT MotherboardID FROM GmSlotCompatibleWith WHERE GPUID = @id
                    UNION
                    SELECT GPUID FROM GmSlotCompatibleWith WHERE MotherboardID = @id
                ) AS Compatible
                JOIN Products P ON P.ID = Compatible.CompatibleID";

            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return (await _db.Database.SqlQueryRaw<CompatibleWithDTO>(CompatibleWithQuery, parameters)
                                    .ToListAsync())
                                    .Cast<CompatibleWithDTO?>()
                                    .ToList();
                // return await _db.Database.SqlQueryRaw<CompatibleWithDTO>(CompatibleWithQuery, parameters)
                //              .ToListAsync();
                                    
        }
        public async Task<int?> GetByModel(string model)
        {
            const string GetIdBymodelQuery = @"
            SELECT 
                    ID
            FROM
                    Products
            WHERE 
                    model = @model";

            var parameters = new[]
            {
                new SqlParameter("@model", model)
            };
            // return await _db.Database.SqlQueryRaw<int?>(GetIdBymodelQuery, parameters)
            //                          .FirstOrDefaultAsync();
            return await _db.Products
                            .FromSqlRaw(GetIdBymodelQuery, new SqlParameter("@model", model))
                            .Select(p => p.ID)
                            .FirstOrDefaultAsync();
        }
    }
}