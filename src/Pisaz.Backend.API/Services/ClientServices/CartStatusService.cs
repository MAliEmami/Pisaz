using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;
using Pisaz.Backend.API.Interfaces;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class CartStatusService(PisazDB db) : IQueryService<CartStatusDTO>
    {
        private readonly PisazDB _db = db;
        public async Task<IEnumerable<CartStatusDTO>> ListAsync(int id)
        {
            const string CartStatusQuery = @"
                    SELECT 
                            CartNumber,
                            CartStatus,
                            (SELECT COUNT(*) From ShoppingCart WHERE CartStatus = 'active') As NumAvailableCart
                    FROM ShoppingCart 
                    WHERE ID = @id";

            var CartStatusList = await _db.Database
                                        .SqlQueryRaw<CartStatusDTO>(CartStatusQuery, new SqlParameter("@id", id))
                                        .ToListAsync();

            return CartStatusList
            .Select(cs => new CartStatusDTO
            {
                CartNumber = cs.CartNumber,
                CartStatus = cs.CartStatus,
                NumAvailableCart = cs.NumAvailableCart
            }).ToList();
        }
    }
}