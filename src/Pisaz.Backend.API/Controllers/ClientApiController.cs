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
    public class ClientApiController(IGeneralService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO> service) : ControllerBase 
    {
        protected readonly IGeneralService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO> _service = service;

        [HttpPost("add")]
        public async Task<int> Add(ClientAddDTO entity)
        {
            return await _service.AddAsync(entity);
        }

        [HttpPost("list")]
        public async Task<IEnumerable<ClientDTO>> List(int id)
        {
            return await _service.ListAsync(id);
        }
    }
}