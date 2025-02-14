using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.Interfaces;
// using Pisaz.Backend.API.Models.Base;

namespace Pisaz.Backend.API.Controllers.Base
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class BaseApiController<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> : ControllerBase where TEntity : class// GeneralModel
    {
        protected readonly IService<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> _servise;

        public BaseApiController(IService<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> servise)
        {
            _servise = servise;
        }

        [HttpPost("add")]
        public virtual async Task<int> Add(TAddDTO entity)
        {
            return await _servise.AddAsync(entity);
        }

        [HttpPost("list")]
        public virtual async Task<IEnumerable<TEntityDTO>> List(int id)
        {
            return await _servise.ListAsync(id);
        }

        [HttpPut("update/{id}")]
        public virtual async Task<TUpdateDTO> Update(int id, TUpdateDTO addUpdateDto)
        {
            await _servise.UpdateAsync(id, addUpdateDto);
            return addUpdateDto;
        }
    }
}