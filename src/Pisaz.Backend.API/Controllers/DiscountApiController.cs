using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Discount;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Discount;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Discount")]
    public class DiscountApiController(IListService<DiscountCode ,DiscountCodeDTO> service) : ControllerBase
    {
        protected readonly IListService<DiscountCode ,DiscountCodeDTO> _service = service;

        [HttpPost("list")]
        //[Authorize]
        public async Task<IActionResult> List(int id)
        {
            var discountCodes = await _service.ListAsync(id);

            if (discountCodes == null || !discountCodes.Any())
            {
                return NotFound("No discount codes found.");
            }

            return Ok(discountCodes);
        }
    }
}