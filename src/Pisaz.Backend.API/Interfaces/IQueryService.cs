using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IQueryService<TEntityDTO> //where TEntityDTO : class
    {
        Task<IEnumerable<TEntityDTO>> ListAsync(int id);
    }
}