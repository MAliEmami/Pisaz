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
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class PurchaseHistoryService(PurchasehistoryRepository purchasehistory) 
    : IQueryService<Products ,PurchaseHistoryDTO>
    {
        private readonly PurchasehistoryRepository _purchasehistory = purchasehistory;
        public async Task<IEnumerable<PurchaseHistoryDTO>> ListAsync(int id)
        {
            var PurchaseHistoryList = await _purchasehistory.GetByIdAsync(id);
            
            if (PurchaseHistoryList == null) 
            {
                return new List<PurchaseHistoryDTO>();
            }

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