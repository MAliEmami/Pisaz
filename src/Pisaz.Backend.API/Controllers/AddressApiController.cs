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
    public class AddressApiController(IQueryService<Address, AddressDTO> queryServise, ICommandService<Address, AddressAddDTO, AddressUpdateDTO> commandService) 
    : ControllerBase
    {
        protected readonly IQueryService<Address, AddressDTO> _queryServise = queryServise;
        protected readonly ICommandService<Address, AddressAddDTO, AddressUpdateDTO> _commandService = commandService;


        [HttpPost("add")]
        public async Task<Address> Add(AddressAddDTO entity)
        {
            return await _commandService.AddAsync(entity);
        }
        
        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var clientIdClaim = User.FindFirst("ClientID")?.Value;            

            if (string.IsNullOrEmpty(clientIdClaim))
            {
                return Unauthorized("User ID not found in token.");
            }

            if (!int.TryParse(clientIdClaim, out var id))
            {
                return BadRequest("Invalid user ID in token.");
            }

            var address = await _queryServise.ListAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }
    }
}