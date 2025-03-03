using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        [HttpPost("list")]
        public async Task<IActionResult> List(int id)
        {
            var refers = await _service.ListAsync(id);

            if (refers == null || !refers.Any())
            {
                return NotFound("No clients found.");
            }

            return Ok(refers);
        }
        
    }
}