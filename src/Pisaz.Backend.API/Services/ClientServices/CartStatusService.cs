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
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class CartStatusService(ShoppingCartRepository shoppingCart)
    : IQueryService<ShoppingCart, CartStatusDTO>
    {
        private readonly ShoppingCartRepository _shoppingCart = shoppingCart;
        public async Task<IEnumerable<CartStatusDTO>> ListAsync(int id)
        {
            var cartStatus = await _shoppingCart.GetByIdAsync(id);

            if (cartStatus == null)
            {
                return new List<CartStatusDTO>();
            }

            return cartStatus
            .Select(cs => new CartStatusDTO
            {
                CartNumber = cs.CartNumber,
                CartStatus = cs.CartStatus
            }).ToList();
        }
    }
}