using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.ProductsDTOs;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Product.Cart;
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ProductServices
{
    public class CompatibleWithService(CompatibleWithRepository compatibleWith) : IQueryCompatibleService<Products, CompatibleWithDTO>
    {
        private readonly CompatibleWithRepository _compatibleWith = compatibleWith;
        public async Task<IEnumerable<CompatibleWithDTO>> ListAsync(int id)
        {
            var compatible = await _compatibleWith.GetByIdAsync(id);//.ConfigureAwait(false);
            
            if (compatible == null) 
            {
                return new List<CompatibleWithDTO>();
            }

            return compatible
            .Select(c => new CompatibleWithDTO
            {
                Category = c.Category,
                CurrentPrice = c.CurrentPrice,
                Brand = c.Brand,
                Model = c.Model,
                StockCount = c.StockCount

            }).ToList();
        }

        public async Task<int?> GetByModel(string model)
        {
            return  await _compatibleWith.GetByModel(model);
        }
        
    }
}