using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pisaz.Backend.API.Interfaces
{
    public interface IQueryCompatibleService<TEntity, TEntityDTO> where TEntityDTO : class
    {
        Task<IEnumerable<TEntityDTO>> ListAsync(int id);
        Task<int?> GetByModel(string model);
        
    }
}