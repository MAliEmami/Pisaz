using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Clients;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Repositories;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class VIPClientService(IQueryRepository<VIPClientDTO> vIPClient) : IQueryService<VIPClient, VIPClientDTO>
    {
        private readonly IQueryRepository<VIPClientDTO> _vIPClient = vIPClient;
        public async Task<IEnumerable<VIPClientDTO>> ListAsync(int id)
        {
            var refers = await _vIPClient.GetByIdAsync(id);
            
            if (refers == null) 
            {
                return new List<VIPClientDTO>();
            }

            return refers
            .Select(vp => new VIPClientDTO
            {
                VIP_Profit = vp.VIP_Profit,
                DaysRemaining = vp.DaysRemaining
                
            }).ToList();
        }
        
    }
}