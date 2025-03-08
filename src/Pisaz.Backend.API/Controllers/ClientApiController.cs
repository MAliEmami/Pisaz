using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;


namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Client/v1")]
    public class ClientApiController(ICommandService<Client, ClientAddDTO, ClientUpdateDTO> commandService , IQueryService<Client, ClientDTO> queryService) : ControllerBase 
    {
        protected readonly ICommandService<Client, ClientAddDTO, ClientUpdateDTO> _commandService = commandService; 
        protected readonly IQueryService<Client, ClientDTO> _queryService = queryService;
        //protected readonly IClientService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO> _service = service;

        [HttpPost("add")]
        public async Task<IActionResult> Add(ClientAddDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _commandService.AddAsync(entity,-1);
            return Ok(result);
        }

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
            
            var clients = await _queryService.ListAsync(id);

            if (clients == null || !clients.Any())
            {
                return NotFound($"No clients found.");
            }

            return Ok(clients);
        }
    }
}