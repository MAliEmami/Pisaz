using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Cart;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.Product.Cart;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("PurchaseHistory/v1")]
    public class PurchaseHistoryApiController(IQueryService<Products ,PurchaseHistoryDTO> service) : ControllerBase
    {
        protected readonly IQueryService<Products ,PurchaseHistoryDTO> _service = service ;
        
        [HttpPost("list")]
        public async Task<IActionResult> List()
        {
            var clientIdClaim = User.FindFirstValue("ClientID");

            int id = 1;
            
            var refers = await _service.ListAsync(id);

            if (refers == null || !refers.Any())
            {
                return NotFound("No clients found.");
            }

            return Ok(refers);
        }
        
    }
}