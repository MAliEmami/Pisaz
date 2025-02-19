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
    : IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>
    {
        private readonly IRepository<Address> _addresses = addresses;


        public async Task<IEnumerable<AddressDTO>> ListAsync(int id)
        {
            var addresses = await _addresses.GetByIdAsync(id);
            if (addresses == null)
            {
                return new List<AddressDTO>();
            }
            
            return new List<AddressDTO>
            {
                new AddressDTO
                {
                    Province = addresses.Province,
                    Remainder = addresses.Remainder
                }
            };
        }

        public async Task<Address> AddAsync(AddressAddDTO entity)
        {
            var a = new Address
            {
                ID = entity.ID,
                Province = entity.Province,
                Remainder = entity.Remainder
            };
            return await _addresses.AddAsync(a);
        }


        public async Task<Address?> UpdateAsync(int id, AddressUpdateDTO entity)
        {
            var dbAddress = await _addresses.GetByIdAsync(id);
            if (dbAddress != null)
            {
                dbAddress.Province = entity.Province;
                dbAddress.Remainder = entity.Remainder;
                return await _addresses.UpdateAsync(dbAddress);
            }
            return dbAddress;
        }

    }
}