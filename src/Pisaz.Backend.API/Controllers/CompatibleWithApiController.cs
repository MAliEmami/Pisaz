using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ProductsDTOs;
using Pisaz.Backend.API.Filters;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Product.Cart;
using Pisaz.Backend.API.Services.ProductServices;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Authorize]
    [VIP]
    [Route("Sazgaryab/v1")] 
    public class CompatibleWithApiController(IQueryCompatibleService<Products, CompatibleWithDTO> service) : ControllerBase
    {
        private readonly IQueryCompatibleService<Products, CompatibleWithDTO> _service = service;
        [HttpPost("list")]
        public async Task<IActionResult> List(string model)
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
            
            var productsID = await _service.GetByModel(model);

            if (productsID == null)
            {
                return NotFound("No Products found.");
            }

            var compatibleWith = await _service.ListAsync(productsID ?? 0);

            return Ok(compatibleWith.ToList());
        }
    }
}