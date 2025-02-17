using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;


namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Client")]
    public class ClientApiController(IService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO> servise) : ControllerBase 
        //: BaseApiController<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>(servise)
    {
        protected readonly IService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO> _servise = servise;


        [HttpPost("add")]
        public async Task<int> Add(ClientAddDTO entity)
        {
            return await _servise.AddAsync(entity);
        }

        [HttpPost("list")]
        public async Task<IEnumerable<ClientDTO>> List(int id)
        {
            return await _servise.ListAsync(id);
        }
    }
}