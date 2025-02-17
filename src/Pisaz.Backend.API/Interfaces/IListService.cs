using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IListService<TEntity, TEntityDTO> where TEntity : class
    {
        Task<IEnumerable<TEntityDTO>> ListAsync(int id);
    }
}