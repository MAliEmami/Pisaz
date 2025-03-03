using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.ReferalCode;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using Pisaz.Backend.API.Services.ClientServices;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("RefersSystem/v1")]
    public class RefersSystemApiController(IQueryService<Refers, RefersDTO> service) : ControllerBase
    {
        protected readonly IQueryService<Refers, RefersDTO> _service = service ;

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