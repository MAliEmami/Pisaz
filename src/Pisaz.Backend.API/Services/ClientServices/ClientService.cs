using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Microsoft.EntityFrameworkCore;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Microsoft.Data.SqlClient;
using Pisaz.Backend.API.DbContextes;
using Pisaz.Backend.API.Repositories;


namespace Pisaz.Backend.API.Services.ClientServices
{
    public class ClientService(IQueryRepository<ClientDTO> queryClients, ICommandRepository<Client> commandClients) 
    : ICommandService<Client, ClientAddDTO, ClientUpdateDTO>, IQueryService<Client, ClientDTO> 
    {
        private readonly IQueryRepository<ClientDTO> _queryClients = queryClients;
        private readonly ICommandRepository<Client> _commandClients = commandClients;

        public async Task<IEnumerable<ClientDTO>> ListAsync(int id)
        {
            var clientInfoList = await _queryClients.GetByIdAsync(id);
            
            if (clientInfoList == null) 
            {
                return new List<ClientDTO>();
            }

            return clientInfoList
            .Select(c => new ClientDTO
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                WalletBalance = c.WalletBalance,
                SignupDate = c.SignupDate
            });
        }
        public async Task<Client> AddAsync(ClientAddDTO entity , int id = -1)
        {
            var c = new Client
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber,
                WalletBalance = default,
                ReferralCode = default,
                SignupDate = DateTime.Now
            };
            return await _commandClients.AddAsync(c);
        }

        public async Task<Client?> UpdateAsync(int id, ClientUpdateDTO entity)
         {
            return null;
            // var dbClient = await _clients.GetByIdAsync(id);
            // if (dbClient != null)
            // {
            //     dbClient.FirstName = entity.FirstName;
            //     dbClient.LastName = entity.LastName;
            //     dbClient.PhoneNumber = entity.PhoneNumber;
            //     return await _clients.UpdateAsync(dbClient);
            // }
            //  return dbClient;
         }
    }
}