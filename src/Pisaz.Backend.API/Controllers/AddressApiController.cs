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
    public class AddressApiController(IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise) : ControllerBase 
    {
        protected readonly IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> _servise = servise;


        [HttpPost("add")]
        public async Task<Address> Add(AddressAddDTO entity)
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