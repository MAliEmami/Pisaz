using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Services;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Address/v1")]
    public class AddressApiController(IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise) : ControllerBase
    {
        protected readonly IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> _service = servise;


        [HttpPost("add")]
        //[Authorize]
        public async Task<Address> Add(AddressAddDTO entity)
        {
            return await _service.AddAsync(entity);
        }
        
        //[Authorize]
        [HttpPost("list")]
        public async Task<IActionResult> List()
        {
            //var clientIdClaim = User.FindFirst("ClientID")?.Value;
            //var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var clientIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;


            Console.WriteLine("Check error: ");
            Console.WriteLine(clientIdClaim);
            Console.WriteLine("finish chkeck");

            if (string.IsNullOrEmpty(clientIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            // Parse the ID to an integer
            if (!int.TryParse(clientIdClaim, out var id))
            {
                return BadRequest("Invalid user ID in token.");
            }

            // Use the ID to fetch the address
            var address = await _service.ListAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }
    }
}