using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Discount;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Discount/v1")]
    public class DiscountApiController(IQueryService<DiscountCode ,DiscountCodeDTO> service) : ControllerBase
    {
        protected readonly IQueryService<DiscountCode ,DiscountCodeDTO> _service = service;

        [HttpPost("list")]
        //[Authorize]
        public async Task<IActionResult> List()
        {
            var clientIdClaim = User.FindFirstValue("ClientID");

            int id = 1;

            var discountCodes = await _service.ListAsync(id);

            if (discountCodes == null || !discountCodes.Any())
            {
                return NotFound("No discount codes found.");
            }

            return Ok(discountCodes);
        }
    }
}