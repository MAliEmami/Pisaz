using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Repositories
{
    public class ShoppingCartRepository(PisazDB db) //: IQueryRepository<ShoppingCart>
    {
        private readonly PisazDB _db = db;
        public async Task<List<CartStatusDTO?>> GetByIdAsync(int id)
        {
            const string CartStatusQuery = @"
                    SELECT 
                            CartNumber,
                            CartStatus,
                            (SELECT COUNT(*) From ShoppingCart WHERE CartStatus = 'active') As NumAvailableCart
                    FROM ShoppingCart 
                    WHERE ID = @id";
                    
            var parameters = new[]
            {
                new SqlParameter("@Id", id)
            };
            return (await _db.Database.SqlQueryRaw<CartStatusDTO>(CartStatusQuery, parameters)
                                    .ToListAsync())
                                    .Cast<CartStatusDTO?>()
                                    .ToList();
                                    
        }
    }
}