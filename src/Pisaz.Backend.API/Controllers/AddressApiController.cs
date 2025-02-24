using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Services;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Adderss")]
    public class AddressApiController(IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise, JwtTokenService jwtTokenService) : ControllerBase 
    {
        protected readonly IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> _service = servise;


        [HttpPost("add")]
        //[Authorize]
        public async Task<Address> Add(AddressAddDTO entity)
        {
            return await _service.AddAsync(entity);
        }

        [HttpPost("list")]
        [Authorize]
        public async Task<IActionResult> List()
        {
            int userId = jwtTokenService.GetUserId(HttpContext);
            
            var addresses = await _service.ListAsync(userId);

            if (addresses == null || !addresses.Any())
            {
                return NotFound("No addresses found.");
            }

            return Ok(addresses);
        }

    }
}