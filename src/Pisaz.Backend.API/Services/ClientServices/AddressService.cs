using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Services.ClientServices
{
    public class AddressService(IRepository<Address> addresses)
    : IQueryService<Address, AddressDTO>, ICommandService<Address, AddressAddDTO, AddressUpdateDTO>
    {
        private readonly IRepository<Address> _addresses = addresses;


        public async Task<IEnumerable<AddressDTO>> ListAsync(int id)
        {
            var addresses = await _addresses.GetByIdAsync(id);

            if (addresses == null) 
            {
                return new List<AddressDTO>();
            }

            return addresses.Select(a => new AddressDTO
            {
                Province = a.Province,
                Remainder = a.Remainder
            }).ToList();
        }

        public async Task<Address> AddAsync(AddressAddDTO entity, int ClientID)
        {
            var a = new Address
            {
                ID = ClientID,
                Province = entity.Province,
                Remainder = entity.Remainder
            };
            return await _addresses.AddAsync(a);
        }


        public async Task<Address?> UpdateAsync(int id, AddressUpdateDTO entity)
        {
            return null;
            // var dbAddress = await _addresses.GetByIdAsync(id);
            // if (dbAddress != null)
            // {
            //     dbAddress.Province = entity.Province;
            //     dbAddress.Remainder = entity.Remainder;
            //     return await _addresses.UpdateAsync(dbAddress);
            // }
            // return dbAddress;
        }

    }
}