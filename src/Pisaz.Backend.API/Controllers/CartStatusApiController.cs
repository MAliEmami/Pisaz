using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("CastSatus/v1")]
    public class CartStatusApiController(IQueryService<ShoppingCart, CartStatusDTO> service) : ControllerBase
    {
        protected readonly IQueryService<ShoppingCart, CartStatusDTO> _service = service;
        
        [Authorize]
        [HttpPost("list")]
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

            var cartStatus = await _service.ListAsync(id);

            if (cartStatus == null)
            {
                return NotFound("No clients found.");
            }

            return Ok(cartStatus);
        }
        
    }
}