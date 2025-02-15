using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Pisaz.Backend.API.Controllers.Base;
using Pisaz.Backend.API.DTOs.Clients;
using Pisaz.Backend.API.DTOs.Clients.SignIn;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Client")]
    public class ClientApiController(IService<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO> servise)
        : BaseApiController<Client, ClientDTO, ClientAddDTO, ClientUpdateDTO>(servise)
    {
    }
}