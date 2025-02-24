using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class AddressApiController(IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise) : ControllerBase
    {
        protected readonly IGeneralService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> _service = servise;


        [HttpPost("add")]
        //[Authorize]
        public async Task<Address> Add(AddressAddDTO entity)
        {
            return await _service.AddAsync(entity);
        }
        
        [Authorize]
        [HttpPost("list")]
        public async Task<IActionResult> List()
        {
            // Extract the user ID from the token claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int Id))
            {
                return Unauthorized("Invalid token.");
            }

            // Call your service with the extracted ID
            var addresses = await _service.ListAsync(Id);

            if (addresses == null || !addresses.Any())
            {
                return NotFound("No addresses found.");
            }

            return Ok(addresses);
        }


        // //[Authorize]
        // [HttpPost("list")]
        // public async Task<IActionResult> List()
        // {
        //     Console.WriteLine("Before assign JWT");
        //     int userId = jwtTokenService.GetUserId(HttpContext);

        //     Console.WriteLine("this is my id: "); // check
        //     Console.WriteLine(userId); // check
        //     Console.WriteLine(" what fuchk"); // check

        //     var addresses = await _service.ListAsync(userId);

        //     if (addresses == null || !addresses.Any())
        //     {
        //         return NotFound("No addresses found.");
        //     }

        //     return Ok(addresses);
        // }
    }
}