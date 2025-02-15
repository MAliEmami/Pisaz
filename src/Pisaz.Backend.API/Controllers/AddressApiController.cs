using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pisaz.Backend.API.DTOs.ClientsDTOs.Dashboard;
using Pisaz.Backend.API.Interfaces;
using Pisaz.Backend.API.Models.ClientModels;

namespace Pisaz.Backend.API.Controllers
{
    [ApiController]
    [Route("Adderss")]
    public class AddressApiController(IService<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO> servise)
        : BaseApiController<Address, AddressDTO, AddressAddDTO, AddressUpdateDTO>(servise)
        {
        }
}