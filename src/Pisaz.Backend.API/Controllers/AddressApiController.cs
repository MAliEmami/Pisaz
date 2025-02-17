using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Adderss")]
    public class AddressApiController(IService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise) : ControllerBase 
        //: BaseApiController<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>(servise)
    {
        protected readonly IService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> _servise = servise;

        // public AddressApiController(IService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise)
        // {
        //     _servise = servise;
        // }

        [HttpPost("add")]
        public async Task<int> Add(AddressAddDTO entity)
        {
            return await _servise.AddAsync(entity);
        }

        [HttpPost("list")]
        public async Task<IEnumerable<AddressDTO>> List(int id)
        {
            return await _servise.ListAsync(id);
        }

    }
}