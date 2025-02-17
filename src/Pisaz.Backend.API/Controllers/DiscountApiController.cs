using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<DiscountCodeDTO>> List(int id)
        {
            return await _service.ListAsync(id);
        }
    }
}