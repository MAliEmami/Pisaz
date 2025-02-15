using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
// using Pisaz.Backend.API.Models.Base;

public class BaseApiController<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> : ControllerBase where TEntity : class
{
    protected readonly IService<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> _servise;

    public BaseApiController(IService<TEntity, TEntityDTO, TAddDTO, TUpdateDTO> servise)
    {
        _servise = servise;
    }

    [HttpPost("add")]
    public async Task<int> Add(TAddDTO entity)
    {
        return await _servise.AddAsync(entity);
    }

    [HttpPost("list")]
    public async Task<IEnumerable<TEntityDTO>> List(int id)
    {
        return await _servise.ListAsync(id);
    }

    // [HttpPut("update/{id}")]
    // public async Task<ClientUpdateDTO> Update(int id, TUpdateDTO addUpdateDto)
    // {
    //     await _servise.UpdateAsync(id, addUpdateDto);
    //     return addUpdateDto;
    // }
}

// public class BaseApiController<TEntity, TEntityDTO> : ControllerBase where TEntity : class
// {
//     protected readonly IAddressService<TEntity, TEntityDTO> _servise;

//     public BaseApiController(IAddressService<TEntity, TEntityDTO> servise)
//     {
//         _servise = servise;
//     }

//     [HttpPost("add")]
//     public async Task<int> Add(TEntityDTO entity)
//     {
//         return await _servise.AddAsync(entity);
//     }

//     [HttpPost("list")]
//     public async Task<IEnumerable<TEntityDTO>> List(int id)
//     {
//         return await _servise.ListAsync(id);
//     }

    // [HttpPut("update/{id}")]
    // public async Task<ClientUpdateDTO> Update(int id, TUpdateDTO addUpdateDto)
    // {
    //     await _servise.UpdateAsync(id, addUpdateDto);
    //     return addUpdateDto;
    // }
//}