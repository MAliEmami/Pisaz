using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Microsoft.EntityFrameworkCore;


namespace Pisaz.Backend.API.Services.ClientServices
{
    public class ClientService(IRepository<Client> clients) : IService<Client, ClientDTO, ClientSignInDTO, ClientUpdateDTO>
    {
        private readonly IRepository<Client> _clients = clients;

        public async Task<int> AddAsync(ClientSignInDTO entity)
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
            return await _clients.AddAsync(c);
        }

        public async Task<IEnumerable<ClientDTO>> ListAsync(int id)
        {
            var clients = await _clients.GetByIdAsync(id);
            return clients
                .Select(c => new ClientDTO
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    WalletBalance = c.WalletBalance,
                    ReferralCode = c.ReferralCode,
                    SignupDate = c.SignupDate
                })
                .ToList();
        }
        // public async Task<IEnumerable<ClientDTO>> ListAsync(int id)
        // {
        //     return _clients
        //         .GetAll()
        //         .Where(c => c.Id == id)
        //         .Select(c => new ClientDTO
        //         {
        //             FirstName = c.FirstName,
        //             LastName = c.LastName,
        //             PhoneNumber = c.PhoneNumber,
        //             WalletBalance = c.WalletBalance,
        //             ReferralCode = c.ReferralCode,
        //             SignUpDate = c.SignUpDate
        //         })
        //         .ToList();
        // }

        public async Task<int> UpdateAsync(int id, ClientUpdateDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}