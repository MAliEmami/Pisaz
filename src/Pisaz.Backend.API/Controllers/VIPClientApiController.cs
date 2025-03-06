using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Clients;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("VIPInfo/v1")]
    public class VIPClientApiController(IQueryService<VIPClient, VIPClientDTO> service) : ControllerBase
    {
        protected readonly IQueryService<VIPClient, VIPClientDTO> _service = service ;

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